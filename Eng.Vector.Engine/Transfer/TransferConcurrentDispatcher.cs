#region Namespaces

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Eng.Vector.Domain.Model;
using Eng.Vector.Util;

#endregion

namespace Eng.Vector.Engine.Transfer
{
    internal class TransferConcurrentDispatcher
    {
        #region Fields

        private static readonly object synchObject = new Object();
        private static TransferConcurrentDispatcher instance;

        private readonly IDictionary<ElaborationKey, ConcurrentQueue<TransferFilePath>> _elaborationDictionary;
        private readonly TreatedConcurrentQueue<TransferFilePath> _treatedFiles;

        #endregion

        #region Ctor(s)

        private TransferConcurrentDispatcher()
        {
            _elaborationDictionary = new ConcurrentDictionary<ElaborationKey, ConcurrentQueue<TransferFilePath>>(new ElaborationKey.EqualityComparer());
            _treatedFiles = new TreatedConcurrentQueue<TransferFilePath>();
        }

        #endregion

        #region Factory

        internal static TransferConcurrentDispatcher Factory
        {
            get
            {
                if (instance == null)
                {
                    lock (synchObject)
                    {
                        if (instance == null)
                        {
                            TransferConcurrentDispatcher newInstance = new TransferConcurrentDispatcher();
                            Thread.MemoryBarrier();
                            instance = newInstance;
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region Internal Methods and Operators

        internal void AddFiles(ElaborationKey key, IEnumerable<TransferFilePath> files)
        {
            Optimize();

            foreach (TransferFilePath file in files)
            {
                AddFile(key, file);
            }
        }

        internal TransferFilePath GetFile(ElaborationKey key)
        {
            TransferFilePath file;
            if (_elaborationDictionary[key].TryDequeue(out file))
            {
                _treatedFiles.Enqueue(file);
                return file;
            }
            throw new KeyNotFoundException();
        }

        #endregion

        #region Methods

        private void AddFile(ElaborationKey key, TransferFilePath file)
        {
            if (!_elaborationDictionary.ContainsKey(key))
            {
                _elaborationDictionary.Add(key, new ConcurrentQueue<TransferFilePath>());
            }

            if (!FileExist(file))
            {
                _elaborationDictionary[key].Enqueue(file);
            }
        }

        private bool FileExist(TransferFilePath file)
        {
            bool isToTreat = false;

            if (_elaborationDictionary != null)
            {
                if (_elaborationDictionary.Any(elaboration => elaboration.Value.Any(x => x == file)))
                {
                    isToTreat = true;
                }
            }

            bool isAlreadyTreated = _treatedFiles.Any(x => x == file);

            return isToTreat || isAlreadyTreated;
        }

        private void Optimize()
        {
            foreach (ElaborationKey elaborationKey in _elaborationDictionary.Keys)
            {
                // Ensure that the elaboration was created by more than AppConfigurationHelper.ElaborationRenewalInterval minutes
                if (elaborationKey.CreatedOn > elaborationKey.CreatedOn.AddMinutes(Helper.Factory.Of.Vector.AppConfiguration.ElaborationRenewalInterval))
                {
                    // Check if queue is empty
                    if (_elaborationDictionary[elaborationKey].Count == 0)
                    {
                        _elaborationDictionary.Remove(elaborationKey);
                    }
                }
            }
        }

        #endregion
    }

    internal class TreatedConcurrentQueue<T> : ConcurrentQueue<T>
    {
        internal new void Enqueue(T item)
        {
            // Algoritmo di scodamento preventivo
            if (Count >= Helper.Factory.Of.Vector.AppConfiguration.ThresholdTailing)
            {
                T result;
                TryDequeue(out result);
            }
            base.Enqueue(item);
        }
    }
}
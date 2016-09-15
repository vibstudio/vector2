#region Namespaces

using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Eng.Vector.Domain.Model;
using Eng.Vector.Util;

#endregion

namespace Eng.Vector.Engine.Transfer
{
    internal class TransferDispatcher
    {
        #region Fields

        private static readonly object synchObject = new object();
        private static TransferDispatcher instance;

        private readonly IDictionary<ElaborationKey, Queue<TransferFilePath>> _elaborationDictionary;
        private readonly TreatedQueue<TransferFilePath> _treatedFiles;

        #endregion

        #region Ctor(s)

        private TransferDispatcher()
        {
            _elaborationDictionary = new Dictionary<ElaborationKey, Queue<TransferFilePath>>(new ElaborationKey.EqualityComparer());
            _treatedFiles = new TreatedQueue<TransferFilePath>();
        }

        #endregion

        #region Factory

        internal static TransferDispatcher Factory
        {
            get
            {
                Monitor.Enter(synchObject);

                try
                {
                    return instance ?? (instance = new TransferDispatcher());
                }
                finally
                {
                    Monitor.Exit(synchObject);
                }
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
            Monitor.Enter(_elaborationDictionary);

            try
            {
                TransferFilePath file = _elaborationDictionary[key].Dequeue();
                _treatedFiles.Enqueue(file);

                return file;
            }
            finally
            {
                Monitor.Exit(_elaborationDictionary);
            }
        }

        #endregion

        #region Methods

        private void AddFile(ElaborationKey key, TransferFilePath file)
        {
            Monitor.Enter(_elaborationDictionary);

            try
            {
                if (!_elaborationDictionary.ContainsKey(key))
                {
                    _elaborationDictionary.Add(key, new Queue<TransferFilePath>());
                }

                if (!FileExist(file))
                {
                    _elaborationDictionary[key].Enqueue(file);
                }
            }
            finally
            {
                Monitor.Exit(_elaborationDictionary);
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

    internal class TreatedQueue<T> : Queue<T>
    {
        #region Methods

        internal new void Enqueue(T item)
        {
            if (Count >= Helper.Factory.Of.Vector.AppConfiguration.ThresholdTailing)
            {
                Dequeue();
            }
            base.Enqueue(item);
        }

        #endregion
    }
}
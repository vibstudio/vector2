#region Namespaces

using System;
using System.Diagnostics;

using Eng.Vector.Domain.Model.Transfer;
using Eng.Vector.Domain.Repositories;
using Eng.Vector.Util;

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Eng.Vector.Framework.UnitTest
{
    [TestClass]
    public class RepositoryTest
    {
        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod]
        public void Add()
        {
            const string EIS_ID = "BEAM";
            const string FILE_CODE = "ACQLETBEAM";
            const string FILE_NAME = "Prova_Vector_2.csv";
            const string CSV = "COL1;COL2;COL3;COL4;COL5\r\nVAL1;VAL2;VAL3;VAL4;VAL5\r\n";

            OperationResult result;

            try
            {
                using (ITransferInputRepository target = Helper.Factory.Of.Vector.DBConnection.GetTransferInputRepository())
                {
                    TransferMessageFile item = new TransferMessageFile
                                               {
                                                   EisID = EIS_ID,
                                                   Code = FILE_CODE,
                                                   Content = CSV.ToByteArray(),
                                                   Name = FILE_NAME,
                                                   IsZipped = false
                                               };

                    result = target.Add(item);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(string.Format("Exception: {0}", exception), "RepositoryTest.Add");

                result = OperationResult.CreateFailure(exception.Message);
            }

            Assert.AreEqual(true, result.IsPerformed);
        }

        ///<summary>
        ///A test for Find
        ///</summary>
        [TestMethod]
        public void Find()
        {
            const string EIS_ID = "BEAM";

            TransferOutputMessage result;

            try
            {
                using (ITransferOutputRepository target = Helper.Factory.Of.Vector.DBConnection.GeTransferOutputRepository(EIS_ID))
                {
                    result = target.Find(EIS_ID);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(string.Format("Exception: {0}", exception), "RepositoryTest.Add");

                result = new TransferOutputMessage(false, exception.Message);
            }

            Assert.AreEqual(true, result.IsPerformed);
        }

        [TestMethod]
        public void NotifyTransferState()
        {
            const string EIS_ID = "BEAM";
            const string FILE_ID = "57";
            const bool TRANSFER_PERFORMED = false;
            const string FAILURE_MESSAGE = "Test failure message";

            OperationResult result;

            try
            {
                using (ITransferOutputRepository target = Helper.Factory.Of.Vector.DBConnection.GeTransferOutputRepository(EIS_ID))
                {
                    result = target.NotifyTransferState(new TransferMessageFile
                                                        {
                                                            ID = FILE_ID,
                                                            TransferPerformed = TRANSFER_PERFORMED,
                                                            Message = FAILURE_MESSAGE
                                                        });
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(string.Format("Exception: {0}", exception), "RepositoryTest.NotifyTransferState");

                result = new TransferOutputMessage(false, exception.Message);
            }

            Assert.AreEqual(true, result.IsPerformed);
        }

        [TestMethod]
        public void IsHealthy()
        {
            bool isHealthy;

            try
            {
                using (ICheckingRepository target = Helper.Factory.Of.Vector.DBConnection.GetCheckingRepository())
                {
                    isHealthy = target.IsHealthy;
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(string.Format("Exception: {0}", exception), "RepositoryTest.NotifyTransferState");

                isHealthy = false;
            }

            Assert.IsTrue(isHealthy);
        }
    }
}
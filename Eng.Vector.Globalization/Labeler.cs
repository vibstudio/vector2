#region Namespaces

using System;
using System.Collections.Generic;
using System.Text;

using Eng.Globalization;
using Eng.Vector.Domain.Model;

#endregion

namespace Eng.Vector.Globalization
{
    public class Labeler : INILabelingProvider, ILabeling
    {
        //#region Ctor(s)
        //
        //public Labeler()
        //    : base(string.Empty, "", "lang")
        //{
        //}
        //
        //#endregion

        #region ILabeling implementation

        public Label CsvMalformed
        {
            get
            {
                string label = this["CsvMalformed"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.CsvMalformed);
            }
        }

        public Label Eis(bool pluralize = false)
        {
            string label = pluralize ? this["Eiss"] : this["Eis"];
            return Label.Create(!string.IsNullOrEmpty(label) ? label : pluralize ? DefaultLang.Eiss : DefaultLang.Eis);
        }

        public Label EisConfigurationParsingFailed(string xpath)
        {
            // Eis configuration fails parsing near {0}

            string labelFormat = this["EisConfigurationParsingFailed"];
            
            return
                Label.Create(!string.IsNullOrEmpty(labelFormat)
                    ? string.Format(labelFormat, xpath)
                    : string.Format(DefaultLang.EisConfigurationParsingFailed, xpath));
        }

        public Label RulesNotRecognized(string str, string[] candidateRules = null)
        {
            // Rules {0}not recognized in the string {1}
            
            string labelFormat = this["RulesNotRecognized"];
            string safeLabelFormat = !string.IsNullOrEmpty(labelFormat) ? labelFormat : DefaultLang.RulesNotRecognized;
            
            if (candidateRules == null)
            {
                return Label.Create(string.Format(safeLabelFormat, string.Empty, str));
            }

            StringBuilder rulesBuilder = new StringBuilder();
            rulesBuilder.Append("(");

            for (int i = 0; i < candidateRules.Length; i++)
            {
                rulesBuilder.AppendFormat(i != candidateRules.Length - 1 ? "{0}, " : "{0}", candidateRules[i]);
            }

            rulesBuilder.Append(") ");

            return Label.Create(string.Format(safeLabelFormat, rulesBuilder, str));
        }

        public Label ConfigurationFileNotFound(ConfigurationScope scope)
        {
            // Configuration file of the {0} not found

            string labelFormat = this["ConfigurationFileNotFound"];
            string textualScope = scope == ConfigurationScope.Application ? Enum.GetName(typeof(ConfigurationScope), scope) : Eis(true).ToString();

            return
                Label.Create(!string.IsNullOrEmpty(labelFormat)
                    ? string.Format(labelFormat, scope)
                    : string.Format(DefaultLang.ConfigurationNotFound, textualScope));
        }

        public Label ConfigurationNotFound(string configurationName)
        {
            // Configuration for {0} not found

            string labelFormat = this["ConfigurationNotFound"];

            return
                Label.Create(!string.IsNullOrEmpty(labelFormat)
                    ? string.Format(labelFormat, configurationName)
                    : string.Format(DefaultLang.ConfigurationNotFound, configurationName));
        }

        public Label ValidationFailed(IList<string> messages)
        {
            // During the broadcast there were validation errors: {0}

            string labelFormat = this["ValidationFailed"];

            string label = !string.IsNullOrEmpty(labelFormat)
                ? ComposeMessage(labelFormat, messages)
                : ComposeMessage(DefaultLang.ValidationFailed, messages);

            return Label.Create(label);
        }

        public Label DumpFailed(string fileName, string message)
        {
            // Cannot move the file {0} in the dump folder

            string labelFormat = this["DumpFailed"];

            string header = !string.IsNullOrEmpty(labelFormat)
                ? string.Format(labelFormat, fileName)
                : string.Format(DefaultLang.DumpFailed, fileName);

            return Label.Create(ComposeMessage(header, message));
        }

        public Label DBProcessingFailed(string fileName, string message)
        {
            // For the file {0} occurred the following processing error in the DB

            string labelFormat = this["DBProcessingFailed"];

            string header = !string.IsNullOrEmpty(labelFormat)
                ? string.Format(labelFormat, fileName)
                : string.Format(DefaultLang.DBProcessingFailed, fileName);

            return Label.Create(ComposeMessage(header, message));
        }
        
        public Label TransmissionInProgress
        {
            get
            {
                string label = this["TransmissionInProgress"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.TransmissionInProgress);
            }
        }

        public Label TransmissionSuccessful
        {
            get
            {
                string label = this["TransmissionSuccessful"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.TransmissionSuccessful);
            }
        }

        public Label OperationPerformed
        {
            get
            {
                string label = this["OperationPerformed"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.OperationPerformed);
            }
        }

        public Label OperationFailed(string message)
        {
            // Operation failed with error: {0}

            string labelFormat = this["OperationFailed"];

            string label = !string.IsNullOrEmpty(labelFormat)
                               ? ComposeMessage(labelFormat, message)
                               : ComposeMessage(DefaultLang.OperationFailed, message);

            return Label.Create(label);
        }

        public Label OperationFailed(IList<string> messages)
        {
            // Operation failed with errors: {0}

            string labelFormat = this["OperationFailedList"];

            string label = !string.IsNullOrEmpty(labelFormat)
                               ? ComposeMessage(labelFormat, messages)
                               : ComposeMessage(DefaultLang.OperationFailedList, messages);

            return Label.Create(label);
        }

        public Label TextualValidationFailed
        {
            get
            {
                string label = this["TextualValidationFailed"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.TextualValidationFailed);
            }
        }

        public Label FileFormatValidationFailed(string extension)
        {
            // The file is not in the {0} format

            string labelFormat = this["FileFormatValidationFailed"];
            string safeLabelFormat = !string.IsNullOrEmpty(labelFormat) ? labelFormat : DefaultLang.FileFormatValidationFailed;
            return Label.Create(string.Format(safeLabelFormat, extension.ToUpper()));
        }

        public Label FileFormatValidationFailed(string[] extensions)
        {
            // The file is not in the expected formats: {0}

            if (extensions.Length == 1) return FileFormatValidationFailed(extensions[0]);

            string labelFormat = this["FileFormatValidationFailedList"];
            string safeLabelFormat = !string.IsNullOrEmpty(labelFormat) ? labelFormat : DefaultLang.FileFormatValidationFailedList;
            return Label.Create(ComposeMessage(safeLabelFormat, extensions));
        }

        public Label System
        {
            get
            {
                string label = this["System"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.System);
            }
        }

        public Label WindowsService
        {
            get
            {
                string label = this["WindowsService"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.WindowsService);
            }
        }

        public Label ErrorCaption
        {
            get
            {
                string label = this["ErrorCaption"];
                return Label.Create(!string.IsNullOrEmpty(label) ? label : DefaultLang.ErrorCaption);
            }
        }

        public Label InfoCaption
        {
            get
            {
                string caption = this["InfoCaption"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.InfoCaption);
            }
        }

        public Label QuestionCaption
        {
            get
            {
                string caption = this["QuestionCaption"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.QuestionCaption);
            }
        }

        public Label WarnCaption
        {
            get
            {
                string caption = this["WarnCaption"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.WarnCaption);
            }
        }

        public Label InputPaths
        {
            get
            {
                string caption = this["InputPaths"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.InputPaths);
            }
        }

        public Label RootPath
        {
            get
            {
                string caption = this["RootPath"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.RootPath);
            }
        }

        public Label DumpPath
        {
            get
            {
                string caption = this["DumpPath"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.DumpPath);
            }
        }

        public Label BlackListPath
        {
            get
            {
                string caption = this["BlackListPath"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.BlackListPath);
            }
        }

        public Label OutputPaths
        {
            get
            {
                string caption = this["OutputPaths"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.OutputPaths);
            }
        }

        public Label InputPollingInterval
        {
            get
            {
                string caption = this["InputPollingInterval"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.InputPollingInterval);
            }
        }

        public Label OutputPollingInterval
        {
            get
            {
                string caption = this["OutputPollingInterval"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.OutputPollingInterval);
            }
        }

        public Label DefaultConnectionString
        {
            get
            {
                string caption = this["DefaultConnectionString"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.DefaultConnectionString);
            }
        }

        public Label WordWrap
        {
            get
            {
                string caption = this["WordWrap"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.WordWrap);
            }
        }

        public Label Language
        {
            get
            {
                string caption = this["Language"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Language);
            }
        }

        public Label TraceLevel
        {
            get
            {
                string caption = this["TraceLevel"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.TraceLevel);
            }
        }

        public Label Save
        {
            get
            {
                string caption = this["Save"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Save);
            }
        }

        public Label Run
        {
            get
            {
                string caption = this["Run"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Run);
            }
        }

        public Label Hours
        {
            get
            {
                string caption = this["Hours"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Hours);
            }
        }

        public Label Minutes
        {
            get
            {
                string caption = this["Minutes"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Minutes);
            }
        }

        public Label Seconds
        {
            get
            {
                string caption = this["Seconds"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Seconds);
            }
        }

        public Label Milliseconds
        {
            get
            {
                string caption = this["Milliseconds"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Milliseconds);
            }
        }

        public Label Status
        {
            get
            {
                string caption = this["Status"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Status);
            }
        }

        public Label NotInstalled
        {
            get
            {
                string caption = this["NotInstalled"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.NotInstalled);
            }
        }

        public Label Stopped
        {
            get
            {
                string caption = this["Stopped"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Stopped);
            }
        }

        public Label Running
        {
            get
            {
                string caption = this["Running"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Running);
            }
        }

        public Label StartupMode
        {
            get
            {
                string caption = this["StartupMode"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.StartupMode);
            }
        }

        public Label Manual
        {
            get
            {
                string caption = this["Manual"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Manual);
            }
        }

        public Label Automatic
        {
            get
            {
                string caption = this["Automatic"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Automatic);
            }
        }

        public Label Disabled
        {
            get
            {
                string caption = this["Disabled"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.Disabled);
            }
        }

        public Label TimeConverter
        {
            get
            {
                string caption = this["TimeConverter"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.TimeConverter);
            }
        }

        public Label PollingIntervals
        {
            get
            {
                string caption = this["PollingIntervals"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.PollingIntervals);
            }
        }

        public Label DBAccess
        {
            get
            {
                string caption = this["DBAccess"];
                return Label.Create(!string.IsNullOrEmpty(caption) ? caption : DefaultLang.DBAccess);
            }
        }

        #endregion

        #region Methods

        private static string ComposeMessage(string header, IList<string> messages)
        {
            string safeHeader = (String.Equals(header.Substring(header.Length - 3, 3), "{0}") ? header.Remove(header.Length - 3) : header).TrimEnd();

            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(safeHeader))
            {
                builder.AppendFormat("{0}{1}\n\n",
                                     safeHeader,
                                     safeHeader.Substring(safeHeader.Length - 1).Equals(":") ? string.Empty : ":");
            }
            foreach (string message in messages)
            {
                builder.AppendFormat("- {0}\n", message);
            }
            return builder.ToString();
        }

        private static string ComposeMessage(string header, string message)
        {
            return ComposeMessage(header, new[] { message });
        }

        public static string GetLast(string source, int tailLength)
        {
            return tailLength >= source.Length ? source : source.Substring(source.Length - tailLength);
        }

        #endregion
    }
}
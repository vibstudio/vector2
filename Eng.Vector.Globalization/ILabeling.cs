#region Namespaces

using System.Collections.Generic;

using Eng.Globalization;
using Eng.Vector.Domain.Model;

#endregion

namespace Eng.Vector.Globalization
{
    public interface ILabeling : ILabelingProvider
    {
        Label CsvMalformed { get; }
        Label TransmissionInProgress { get; }
        Label TransmissionSuccessful { get; }
        Label OperationPerformed { get; }
        Label TextualValidationFailed { get; }
        Label System { get; }
        Label WindowsService { get; }
        Label ErrorCaption { get; }
        Label InfoCaption { get; }
        Label QuestionCaption { get; }
        Label WarnCaption { get; }
        Label InputPaths { get; }
        Label RootPath { get; }
        Label DumpPath { get; }
        Label BlackListPath { get; }
        Label OutputPaths { get; }
        Label InputPollingInterval { get; }
        Label OutputPollingInterval { get; }
        Label DefaultConnectionString { get; }
        Label WordWrap { get; }
        Label Language { get; }
        Label TraceLevel { get; }
        Label Save { get; }
        Label Run { get; }
        Label Hours { get; }
        Label Minutes { get; }
        Label Seconds { get; }
        Label Milliseconds { get; }
        Label Status { get; }
        Label NotInstalled { get; }
        Label Stopped { get; }
        Label Running { get; }
        Label StartupMode { get; }
        Label Manual { get; }
        Label Automatic { get; }
        Label Disabled { get; }
        Label TimeConverter { get; }
        Label PollingIntervals { get; }
        Label DBAccess { get; }
        Label Eis(bool pluralize = false);
        Label EisConfigurationParsingFailed(string xpath);
        Label RulesNotRecognized(string str, string[] candidateRules);
        Label ConfigurationFileNotFound(ConfigurationScope scope);
        Label ConfigurationNotFound(string configurationName);
        Label ValidationFailed(IList<string> messages);
        Label DumpFailed(string fileName, string message);
        Label DBProcessingFailed(string fileName, string message);
        Label OperationFailed(string message);
        Label OperationFailed(IList<string> messages);
        Label FileFormatValidationFailed(string extension);
        Label FileFormatValidationFailed(string[] extensions);
    }
}
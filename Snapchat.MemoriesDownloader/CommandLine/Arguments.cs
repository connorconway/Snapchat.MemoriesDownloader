using System;
using Fclp;
using Fclp.Internals;

namespace Snapchat.MemoriesDownloader.CommandLine
{
    public class Arguments
    {
        public static readonly Arguments Help = new Arguments();

        public string MemoriesHistoryFilePath { get; set; }
        public string OutputDirectoryPath { get; set; }

        public static Arguments Parse(params string[] args)
        {
            var commandLineParser = new FluentCommandLineParser<Arguments> {IsCaseSensitive = false};
            var otherInvalidOccurrence = false;

            commandLineParser.SetupHelp("?", "help").Callback(text => Console.WriteLine(text));

            commandLineParser.Setup(m => m.MemoriesHistoryFilePath)
                             .As('m', "memoriesfile")
                             .CaptureAdditionalArguments(a => otherInvalidOccurrence = true)
                             .SetDefault("memories_history.json")
                             .WithDescription(" - The path to the json file containing the memories history. By default it will use 'memories_history.json' at the same location as this executable is run.");

            
            commandLineParser.Setup(m => m.OutputDirectoryPath)
                            .As('o', "outputdirectory")
                            .CaptureAdditionalArguments(a => otherInvalidOccurrence = true)
                            .SetDefault("Output")
                            .WithDescription(" - The path to the directory you would like to store the output files. By default, it will be stored in 'Output' at the same location as this executable is run.");

            var result = commandLineParser.Parse(args);
            if (otherInvalidOccurrence)
            {
                commandLineParser.HelpOption.ShowHelp(commandLineParser.Options);
                return Help;
            }

            if (result.HelpCalled)
                return Help;

            if (result.HasErrors)
                throw new ArgumentException(result.ErrorText);

            return commandLineParser.Object;
        }
    }
}
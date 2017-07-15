using CommandLine;
using CommandLine.Text;

namespace A
{
    class Options
    {
        [Option('s', "start", Required = false,
          HelpText = "this will start the server.")]
        public bool Start { get; set; }
        [Option('q', "stop", Required = false,
          HelpText = "this will stop the server.")]
        public bool Stop { get; set; }

        [Option('p', "slow", DefaultValue = false,
          HelpText = "this will set server to slow mode.")]
        public bool Slow { get; set; }

        [Option('f', "fast", DefaultValue = false,
          HelpText = "this will set server to fast mode.")]
        public bool Fast { get; set; }

        [Option('c', "clients",  HelpText = "this will show all connected clients.")]
        public bool Clients { get; set; }

        [Option('v', "verbose", DefaultValue = false,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; } 


        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using DevGpt.Models.Commands;
using OpenAI.Chat;

namespace DevGpt.Commands.Functions
{

    internal class FunctionAdapter
    {
        private readonly ICommandBase _command;

        public Function GetFunction()
        {
            var rootNode = new JsonObject
            {
                { "type", "object" }
            };
            var properties = new JsonObject();
            var requiredArray = new JsonArray { };

            foreach (var argument in _command.Arguments)
            {
                requiredArray.Add(argument);
                var argumentObject = new JsonObject
                {
                    { "type", "string" },
                    { "description", $"The {argument} argument" }
                };
                properties.Add(argument, argumentObject);
            }
            rootNode.Add("properties", properties);
            rootNode.Add("required", requiredArray);

            return new Function(_command.Name, _command.Description,rootNode);
        }
        public FunctionAdapter(ICommandBase command)
        {
            _command = command;
        }
    }
}

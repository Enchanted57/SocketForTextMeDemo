using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestApp {
    class Program {
        static void Main(string[] args) {
            // Initializes socket and tries to connect
            Socket socket = IO.Socket("http://localhost:3030");

            // sample data for authentication
            var data = new {
                strategy = "local",
                email = "peter@seznam.cz",
                password = "secret"
            };

            JObject obj = JObject.FromObject(data);

            // calling server authentication service
            socket.Emit("authenticate", (error, token) => {
                Console.WriteLine($"error: {error}");
                Console.WriteLine($"token: {token}");

                Console.WriteLine(error == null);
                Console.WriteLine(token == null);
                
            },  obj);

            // listening to the created messages 
            socket.On("messages created", message => {
                Console.WriteLine(message);
            });

            Console.ReadLine();
        }
    }
}

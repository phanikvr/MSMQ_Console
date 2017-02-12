using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using Newtonsoft.Json;
using System.IO;

namespace ConsoleApplication1_msmq
{
    [Serializable]
    public class test_object
    {
        public string Name;
        public string Address;
        public int Age;
        public double Salary;
        public DateTime DateOfBirth;
    }

    class Program
    {
        static void AckMessages()
        {
            MessageQueue msgQ = new MessageQueue(@".\private$\test");
            Message myMessage = new Message();
            myMessage.Body = "Sample";
            myMessage.AdministrationQueue = new MessageQueue(@".\private$\Ack");
            myMessage.AcknowledgeType = AcknowledgeTypes.FullReachQueue
                                        |
            AcknowledgeTypes.FullReceive;
            msgQ.Send(myMessage, MessageQueueTransactionType.Single);
        }

        static void CreateMessageQue()
        {
            //var message_name = @".\private$\test";
            var message_name = @"formatname:DIRECT=OS:machine2\Private$\recievingQueue";
            var mes_q = new MessageQueue(message_name);
            //var message = new Message();
            //message.Body = "this is my first msmq code";
            var obj = new test_object { Address = "waitara" + DateTime.Now, Age = 40, DateOfBirth = DateTime.Now.AddYears(-30), Name = "myname", Salary = 123.00 };
            //mes_q.Formatter = new BinaryMessageFormatter();
            //mes_q.Send(JsonConvert.SerializeObject(obj));
            mes_q.Send(obj);


            //Console.ReadLine();

            //var recv_msg = mes_q.Receive();

            var messages = mes_q.GetAllMessages();
            Console.WriteLine("Total Messages: " + messages.Count());
            foreach (var item in messages)
            {
                Console.WriteLine(item.BodyType);
                if (item.BodyType == 768)
                {
                    var readStream = new StreamReader(item.BodyStream);
                    Console.WriteLine(readStream.ReadToEnd());
                }
                else
                {
                    Console.WriteLine(item.Body);
                }
            }
        }
        static void Main(string[] args)
        {

            //Console.WriteLine(FirstFactorial(8));
            //AckMessages();
            //printBinary(19);
            computeVariation();
            Console.ReadLine();
        }

        public static int FirstFactorial(int num)
        {
            if (num > 0)
                return num * FirstFactorial(num - 1);
            return 1;

        }

        public static void printBinary(int num)
        {
            Console.WriteLine(1);
            for (int i = 2, n = 1; i <= num; i++)
            {
                Console.Write(string.Format("i={0} ", i));
                for (int j = 1; j <= i; j++)
                {
                    if (i == 2 * n + 1 || i == 2 * n)
                    {
                        Console.Write(j % 2 == 0 ? 1 : 0);
                    }
                    else
                        Console.Write(j % 2);
                }
                Console.WriteLine();
                if (i == 2 * n + 1)
                    n += 2;

            }
        }

        
        public static void computeVariation()
        {
            var input = Console.ReadLine();
            int k = Convert.ToInt32(input.Substring(0, input.IndexOf(' ')));
            int size = Convert.ToInt32(input.Substring(input.IndexOf(' '), input.Length - 1));
            var array = Console.ReadLine();
            var arr = new int [size];
            for (int i = 0; i < size; i = input.IndexOf(' '))
            {
                arr[i] = Convert.ToInt32(input.Substring(i, input.IndexOf(' ')));
            }
            
            int variation = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Math.Abs(arr[j] - arr[i]) >= k)
                        variation++;
                }
            }

            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.id = 1;
            obj.id = "asdf";
            Console.WriteLine(obj.id);
            A a = new A();
            Console.WriteLine(variation);
        }
    }

    public class A
    {
        public static string ad;
        public void de()
        {
            ad = "asdf";
        }
    }

    public class b : A
    {
        public void t()
        {
            ad = "asdfasdf";
        }
    }
}

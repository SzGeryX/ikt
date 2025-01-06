using System.Text.Json;

internal class Program
{
    public static string getUserInput(string name, string[] valid, string hint)
    {
        if (hint != "")
        {
            hint = $"({hint})";
        }

        if (valid.Length != 0)
        {
            hint += $"[{string.Join(", ", valid)}]";
        }


        Console.WriteLine($"Enter the components {name}{hint}:");
        string? input = Console.ReadLine();

        while (true)
        {
            if (valid.Length > 0)
            {
               if (Array.IndexOf(valid, input) != -1)
                {
                    break;
                }
            }
            else if (input != null)
            {
                break;
            }

            Console.WriteLine("Invalid input! Try again.");
            input = Console.ReadLine();
        }

        return input;
    }

    abstract class Component 
    {
        public string href {  get; set; }
        public string imgSrc { get; set; }
        public string alt { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int price { get; set; }

        public Component() { }
        public Component(string[] param) //param: type, name, price, desc, imgSrc
        {
            this.href = param[1].ToLower().Replace(" ", "-") + ".html";
            this.imgSrc = param[4];
            this.alt = param[1];
            this.title = param[1];
            this.description = param[3];
            this.type = param[0];
            this.price = Convert.ToInt32(param[2]);
        }

        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}";
        }

        public string Manufacturer()
        {
            return title.Split(" ")[0];
        }

        public static string[] Prompt()
        {
            string[] param = new string[5];

            param[0] = getUserInput("type", ["gpu", "cpu", "ram", "motherboard", "ssd", "mouse", "keyboard", "monitor"], "");
            param[1] = getUserInput("name", [], "");
            param[2] = getUserInput("price", [], "");
            param[3] = getUserInput("description", [], "");
            param[4] = getUserInput("image source", [], "link");

            return param;
        }
    }

    class Shop
    {
        public List<Gpu> gpus { get; set; } = new List<Gpu>();
        public List<Cpu> cpus { get; set; } = new List<Cpu>();
        public List<Ram> rams { get; set; }  = new List<Ram>();
        public List<Motherboard> motherboards { get; set; }  = new List<Motherboard>();
        public List<Ssd> ssds { get; set; }  = new List<Ssd>();
        public List<Mouse> mice { get; set; }  = new List<Mouse>();
        public List<Keyboard> keyboards { get; set; }  = new List<Keyboard>();
        public List<Monitor> monitors { get; set; }  = new List<Monitor>();

        public void ReadFromJson(string filename)
        {
            string jsonString = File.ReadAllText(filename);

            Shop temp = JsonSerializer.Deserialize<Shop>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            this.gpus = temp.gpus ?? new List<Gpu>();
            this.cpus = temp.cpus ?? new List<Cpu>();
            this.rams = temp.rams ?? new List<Ram>();
            this.motherboards = temp.motherboards ?? new List<Motherboard>();
            this.ssds = temp.ssds ?? new List<Ssd>();
            this.mice = temp.mice ?? new List<Mouse>();
            this.keyboards = temp.keyboards ?? new List<Keyboard>();
            this.monitors = temp.monitors ?? new List<Monitor>();
        }

        public void AddComponent()
        {
            static bool confirmationPrompt(string[] param)
            {
                Console.WriteLine($"Would you like to add a component with these parameters?[y/n]: {string.Join(", ", param)}");
                string input = Console.ReadLine();

                while (input != "y" && input != "n")
                {
                    Console.WriteLine(input);
                    Console.WriteLine("Invalid input! Try again.");
                    input = Console.ReadLine();
                }

                if (input == "y")
                    return true;
                else
                    return false;
            }

            string[] basicParam = Component.Prompt();

            switch (basicParam[0])
            {
                case "gpu":
                    {
                        string[] param = basicParam.Concat(Gpu.Prompt()).ToArray();
                        
                        bool unique = true;
                        for (int i = 0; i < gpus.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;
                        
                        if (!confirmationPrompt(param)) break;

                        this.gpus.Add(new Gpu(param));
                        Console.WriteLine("New component added!");

                        break;
                    }
                case "cpu":
                    {
                        string[] param = basicParam.Concat(Cpu.Prompt()).ToArray();
                        
                        bool unique = true;
                        for (int i = 0; i < cpus.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;

                        if (!confirmationPrompt(param)) break;

                        this.cpus.Add(new Cpu(param));
                        Console.WriteLine("New component added!");
                        break;
                    }
                case "ram":
                    {
                        string[] param = basicParam.Concat(Ram.Prompt()).ToArray();

                        bool unique = true;
                        for (int i = 0; i < cpus.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;

                        if (!confirmationPrompt(param)) break;

                        this.rams.Add(new Ram(param));
                        Console.WriteLine("New component added!");
                        break;
                    }
                case "motherboard":
                    {
                        string[] param = basicParam.Concat(Motherboard.Prompt()).ToArray();

                        bool unique = true;
                        for (int i = 0; i < motherboards.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;

                        if (!confirmationPrompt(param)) break;

                        this.motherboards.Add(new Motherboard(param));
                        Console.WriteLine("New component added!");
                        break;
                    }
                case "ssd":
                    { 
                        string[] param = basicParam.Concat(Ssd.Prompt()).ToArray();

                        bool unique = true;
                        for (int i = 0; i < ssds.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;

                        if (!confirmationPrompt(param)) break;

                        this.ssds.Add(new Ssd(param));
                        Console.WriteLine("New component added!");
                        break;
                    }
                case "mouse":
                    {
                        string[] param = basicParam.Concat(Mouse.Prompt()).ToArray();

                        bool unique = true;
                        for (int i = 0; i < mice.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;

                        if (!confirmationPrompt(param)) break;

                        this.mice.Add(new Mouse(param));
                        Console.WriteLine("New component added!");
                        break;
                    }
                case "keyboard":
                    {
                        string[] param = basicParam.Concat(Keyboard.Prompt()).ToArray();

                        bool unique = true;
                        for (int i = 0; i < keyboards.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;

                        if (!confirmationPrompt(param)) break;

                        this.keyboards.Add(new Keyboard(param));
                        Console.WriteLine("New component added!");
                        break;
                    }
                case "monitor":
                    {
                        string[] param = basicParam.Concat(Monitor.Prompt()).ToArray();

                        bool unique = true;
                        for (int i = 0; i < monitors.Count; i++)
                        {
                            if (gpus[i].title == param[1])
                            {
                                Console.WriteLine("Component with the same name already exists.");
                                unique = false;

                                break;
                            }

                        }

                        if (!unique) break;

                        if (!confirmationPrompt(param)) break;

                        this.monitors.Add(new Monitor(param));
                        Console.WriteLine("New component added!");
                        break;
                    }
            }
        }

        public void WriteToJson(string file)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };            
            string json = JsonSerializer.Serialize(this, options);

            File.WriteAllText(file, json);
        }

        public void Search()
        {
            string categories = "";
            string name = "";
            string price = "";

            void menu()
            {
                Console.WriteLine($"[1] - Category \n" +
                    $"[2] - Name \n" +
                    $"[3] - Price \n" +
                    $"[4] - Search");



                string filter = Console.ReadLine(); 

                while (filter == "" || Array.IndexOf(["1", "2", "3", "4"], filter) == -1)
                {
                    Console.WriteLine("Invalid option! Try again.");

                    filter = Console.ReadLine();
                }


                switch (filter)
                {
                    case "1":
                        {
                            categories = getUserInput("types", [], "seperated by ;, leave empty for all types"); // todo: input validation
                            menu();
                            break;
                        }
                    case "2":
                        {
                            name = getUserInput("name", [], "");
                            menu();
                            break;
                        }
                    case "3":
                        {
                            price = getUserInput("price range", [], "e.g., 14000-20000, the range will be interpreted on a closed interval");
                            menu();
                            break;
                        }
                    case "4":
                        {
                            break;
                        }
                }

            }

            menu();

            List<Component> results = new List<Component>();

            string[] categoriesList = categories.Split(';');

            if (Array.IndexOf(categoriesList, "gpu") != -1)
            {
                results.AddRange(gpus);
            }
            else if (Array.IndexOf(categoriesList, "cpu") != -1)
            {
                results.AddRange(cpus);
            }
            else if (Array.IndexOf(categoriesList, "ram") != -1)
            {
                results.AddRange(rams);
            }
            else if (Array.IndexOf(categoriesList, "motherboard") != -1)
            {
                results.AddRange(motherboards);
            }
            else if (Array.IndexOf(categoriesList, "ssds") != -1)
            {
                results.AddRange(ssds);
            }
            else if (Array.IndexOf(categoriesList, "mouse") != -1)
            {
                results.AddRange(mice);
            }
            else if (Array.IndexOf(categoriesList, "keyboard") != -1)
            {
                results.AddRange(keyboards);
            }
            else if (Array.IndexOf(categoriesList, "monitor") != -1)
            {
                results.AddRange(monitors);
            }
            else
            {
                results.AddRange(gpus);
                results.AddRange(cpus);
                results.AddRange(rams);
                results.AddRange(motherboards);
                results.AddRange(ssds);
                results.AddRange(mice);
                results.AddRange(keyboards);
                results.AddRange(monitors);
            }

            List<Component> resultsTemp = new List<Component>();
            foreach (Component component in results)
            {
                if (component.title.Contains(name))
                {
                    resultsTemp.Add(component);
                }
            }

            if (price.Length > 0)
            {
                List<Component> resultsTempTemp = new List<Component>();
                int[] priceRange = [Convert.ToInt32(price.Split("-")[0]), Convert.ToInt32(price.Split("-")[1])];
                foreach (Component component in resultsTemp)
                {
                    if (component.price >= priceRange[0] && component.price <= priceRange[1])
                    {
                        resultsTempTemp.Add(component);
                    }

                }
                foreach (Component component in resultsTempTemp)
                {
                    Console.WriteLine(component.Stringify());
                }
            }
            else
            {
                foreach (Component component in resultsTemp)
                {
                    Console.WriteLine(component.Stringify());
                }
            }


        }

        public void Discount()
        {
            Console.WriteLine("Enter discount amount(e.g., 0.1):");
            double amount = Convert.ToDouble(Console.ReadLine());
            string categories = getUserInput("types", [], "seperated by ;, leave empty for all types"); // todo: input validation
            string[] categoriesList = categories.Split(';');
            
            if (Array.IndexOf(categoriesList, "gpu") != -1)
            {
                foreach (Gpu gpu in this.gpus)
                {
                    gpu.price = Convert.ToInt32(gpu.price * amount);
                }
            }
            else if (Array.IndexOf(categoriesList, "cpu") != -1)
            {
                foreach (Cpu cpu in this.cpus)
                {
                    cpu.price = Convert.ToInt32(cpu.price * amount);
                }
            }
            else if (Array.IndexOf(categoriesList, "ram") != -1)
            {
                foreach (Ram ram in this.rams)
                {
                    ram.price = Convert.ToInt32(ram.price * amount);
                }
            }
            else if (Array.IndexOf(categoriesList, "motherboard") != -1)
            {
                foreach (Motherboard motherboard in this.motherboards)
                {
                    motherboard.price = Convert.ToInt32(motherboard.price * amount);
                }
            }
            else if (Array.IndexOf(categoriesList, "ssds") != -1)
            {
                foreach (Ssd ssd in this.ssds)
                {
                    ssd.price = Convert.ToInt32(ssd.price * amount);
                }
            }
            else if (Array.IndexOf(categoriesList, "mouse") != -1)
            {
                foreach (Mouse mouse in this.mice)
                {
                    mouse.price = Convert.ToInt32(mouse.price * amount);
                }
            }
            else if (Array.IndexOf(categoriesList, "keyboard") != -1)
            {
                foreach (Keyboard keyboard in this.keyboards)
                {
                    keyboard.price = Convert.ToInt32(keyboard.price * amount);
                }
            }
            else if (Array.IndexOf(categoriesList, "monitor") != -1)
            {
                foreach (Monitor monitor in this.monitors)
                {
                    monitor.price = Convert.ToInt32(monitor.price * amount);
                }
            }
            else
            {
                foreach (Gpu gpu in this.gpus)
                {
                    gpu.price = Convert.ToInt32(gpu.price * amount);
                }
                foreach (Cpu cpu in this.cpus)
                {
                    cpu.price = Convert.ToInt32(cpu.price * amount);
                }
                foreach (Ram ram in this.rams)
                {
                    ram.price = Convert.ToInt32(ram.price * amount);
                }
                foreach (Motherboard motherboard in this.motherboards)
                {
                    motherboard.price = Convert.ToInt32(motherboard.price * amount);
                }
                foreach (Ssd ssd in this.ssds)
                {
                    ssd.price = Convert.ToInt32(ssd.price * amount);
                }
                foreach (Mouse mouse in this.mice)
                {
                    mouse.price = Convert.ToInt32(mouse.price * amount);
                }
                foreach (Keyboard keyboard in this.keyboards)
                {
                    keyboard.price = Convert.ToInt32(keyboard.price * amount);
                }
                foreach (Monitor monitor in this.monitors)
                {
                    monitor.price = Convert.ToInt32(monitor.price * amount);
                }
            }
        }

        public void Statistics()
        {
            Console.WriteLine($"There are {gpus.Count} gpus in the database.");
            Dictionary<string, int> gpuDict = new Dictionary<string, int>();
            foreach (Gpu gpu in this.gpus)
            {
                if (gpuDict.ContainsKey(gpu.Manufacturer()))
                {
                    gpuDict[gpu.Manufacturer()]++;
                }
                else
                {
                    gpuDict.Add(gpu.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in gpuDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }


            Console.WriteLine($"There are {cpus.Count} cpus in the database.");
            Dictionary<string, int> cpuDict = new Dictionary<string, int>();
            foreach (Cpu cpu in this.cpus)
            {
                if (cpuDict.ContainsKey(cpu.Manufacturer()))
                {
                    cpuDict[cpu.Manufacturer()]++;
                }
                else
                {
                    cpuDict.Add(cpu.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in cpuDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }


            Console.WriteLine($"There are {rams.Count} rams in the database.");
            Dictionary<string, int> ramDict = new Dictionary<string, int>();
            foreach (Ram ram in this.rams)
            {
                if (ramDict.ContainsKey(ram.Manufacturer()))
                {
                    ramDict[ram.Manufacturer()]++;
                }
                else
                {
                    ramDict.Add(ram.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in ramDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }


            Console.WriteLine($"There are {motherboards.Count} motherboards in the database.");
            Dictionary<string, int> motherboardDict = new Dictionary<string, int>();
            foreach (Motherboard motherboard in this.motherboards)
            {
                if (motherboardDict.ContainsKey(motherboard.Manufacturer()))
                {
                    motherboardDict[motherboard.Manufacturer()]++;
                }
                else
                {
                    motherboardDict.Add(motherboard.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in motherboardDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }


            Console.WriteLine($"There are {ssds.Count} ssds in the database.");
            Dictionary<string, int> ssdDict = new Dictionary<string, int>();
            foreach (Ssd ssd in this.ssds)
            {
                if (ssdDict.ContainsKey(ssd.Manufacturer()))
                {
                    ssdDict[ssd.Manufacturer()]++;
                }
                else
                {
                    ssdDict.Add(ssd.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in ssdDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }


            Console.WriteLine($"There are {mice.Count} mice in the database.");
            Dictionary<string, int> mouseDict = new Dictionary<string, int>();
            foreach (Mouse mouse in this.mice)
            {
                if (mouseDict.ContainsKey(mouse.Manufacturer()))
                {
                    mouseDict[mouse.Manufacturer()]++;
                }
                else
                {
                    mouseDict.Add(mouse.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in mouseDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }


            Console.WriteLine($"There are {keyboards.Count} keyboards in the database.");
            Dictionary<string, int> keyboardDict = new Dictionary<string, int>();
            foreach (Keyboard keyboard in this.keyboards)
            {
                if (keyboardDict.ContainsKey(keyboard.Manufacturer()))
                {
                    keyboardDict[keyboard.Manufacturer()]++;
                }
                else
                {
                    keyboardDict.Add(keyboard.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in keyboardDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }

            
            Console.WriteLine($"There are {monitors.Count} monitors in the database.");
            Dictionary<string, int> monitorDict = new Dictionary<string, int>();
            foreach (Monitor monitor in this.monitors)
            {
                if (monitorDict.ContainsKey(monitor.Manufacturer()))
                {
                    monitorDict[monitor.Manufacturer()]++;
                }
                else
                {
                    monitorDict.Add(monitor.Manufacturer(), 1);
                }
            }
            
            foreach (KeyValuePair<string, int> i in monitorDict)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }
        }
    }

    class Gpu : Component
    {
        public string vram { get; set; }
        public string connection { get; set; }

        public Gpu() { }
        public Gpu(string[] param) : base(param) //param: type, name, price, desc, imgSrc, vram, connection
        {
            this.vram = param[5];
            this.connection = param[6];
        }
        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}|Vram: {this.vram}|Connection: {this.connection}";
        }

        public new static string[] Prompt()
        {
            string[] param = new string[2];

            param[0] = getUserInput("vram", [], "with speed and type e.g., 6GB GDDR6");
            param[1] = getUserInput("connection type", [], "e.g., PCIe 3.0 x16");

            return param;
        }
    }

    class Cpu : Component
    {
        public int coreCount { get; set; }
        public int threadCount { get; set; }
        public string baseClock { get; set; }
        public string cache { get; set; }
        public string socket { get; set; }
        public string architecture { get; set; }

        public Cpu() { }
        public Cpu(string[] param) : base(param) //param: type, name, price, desc, imgSrc, coreCount, threadCound, baseClock, cache, socket, architecture
        {
            this.coreCount = Convert.ToInt32(param[5]);
            this.threadCount = Convert.ToInt32(param[6]);
            this.baseClock = param[7];
            this.cache = param[8];
            this.socket = param[9];
            this.architecture = param[10];
        }
        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}|Core count: {this.coreCount}|Thread count: {this.threadCount}|Base clock speed: {this.baseClock}|Cache: {this.cache}|Socket: {this.socket}|Architecture: {this.architecture}";
        }

        public new static string[] Prompt()
        {
            string[] param = new string[6];

            param[0] = getUserInput("number of cores", [], "");
            param[1] = getUserInput("number of threads", [], "");
            param[2] = getUserInput("base clock speed", [], "with units");
            param[3] = getUserInput("cache size", [], "e.g., 64 MB L3");
            param[4] = getUserInput("socket type", [], "");
            param[5] = getUserInput("architecture", [], "");

            return param;
        } 
    }

    class Ram : Component
    {
        public string capacity { get; set; }
        public string speed { get; set; }
        public string formFactor { get; set; }
        public string memoryType { get; set; }

        public Ram() { }
        public Ram(string[] param) : base(param) //param: type, name, price, desc, imgSrc, capacity, speed, formFactor, memoryType
        {
            this.capacity = param[5];
            this.speed = param[6];
            this.formFactor = param[7];
            this.memoryType = param[8];
        }
        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}|Capacity: {this.capacity}|Speed: {this.speed}|Form factor:{this.formFactor}|Memory type:{this.memoryType}";
        }
        public new static string[] Prompt()
        {
            string[] param = new string[4];

            param[0] = getUserInput("capacity", [], "with units");
            param[1] = getUserInput("speed", [], "with units");
            param[2] = getUserInput("form factor", [], "");
            param[3] = getUserInput("memory type", [], "");

            return param;
        } 
    }

    class Motherboard : Component
    {
        public string chipset { get; set; }
        public string socket { set; get; }
        public int memorySlots { get; set; }
        public string maxMemory { get; set; }
        public string memoryType { set; get; }
        public string maxMemorySpeed { get; set; }
        public string formFactor { get; set; }

        public Motherboard() { }
        public Motherboard(string[] param) : base(param) //param: type, name, price, desc, imgSrc, chipset, socket, memorySlots, maxMemory, memoryType, maxMemorySpeed, fromFactor
        {
            this.chipset = param[5];
            this.socket = param[6];
            this.memorySlots = Convert.ToInt32(param[7]);
            this.maxMemory = param[8];
            this.memoryType= param[9];
            this.maxMemorySpeed = param[10];
            this.formFactor = param[11];
        }

        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}|Chipset: {this.chipset}|Socket: {this.socket}|Memory slots: {this.memorySlots}|Max memory: {this.maxMemory}|Memory type: {this.memoryType}|Max memory speed: {this.maxMemorySpeed}|Form factor: {this.formFactor}";
        }
        public new static string[] Prompt()
        {
            string[] param = new string[7];

            param[0] = getUserInput("chipset", [], "");
            param[1] = getUserInput("socket", [], "");
            param[2] = getUserInput("nubmer of memory slots", [], "");
            param[3] = getUserInput("max memory size", [], "with units");
            param[4] = getUserInput("memory type", [], "");
            param[5] = getUserInput("max memory speed", [], "with units");
            param[6] = getUserInput("form factor", [], "");

            return param;
        } 
       
    }

    class Ssd : Component
    {
        public string capacity { get; set; }
        public string connection { get; set; }
        public string readSpeed { get; set; }
        public string writeSpeed { get; set; }

        public Ssd() { }
        public Ssd(string[] param) : base(param) //param: type, name, price, desc, imgSrc, capacity, connection, readSpeed, writeSpeed
        {
            this.capacity = param[5];
            this.connection = param[6];
            this.readSpeed = param[7];
            this.writeSpeed = param[8];
        }
        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}";
        }
        public new static string[] Prompt()
        {
            string[] param = new string[4];

            param[0] = getUserInput("capacity", [], "with units");
            param[1] = getUserInput("connection type", [], "e.g.,NVMe M.2");
            param[2] = getUserInput("read speed", [], "with units");
            param[3] = getUserInput("write speed", [], "with units");

            return param;
        } 
    }

    class Mouse : Component
    {
        public int dpi { get; set; }
        public bool wireless { get; set; }

        public Mouse() { }
        public Mouse(string[] param) : base(param) //param: type, name, price, desc, imgSrc, dpi, wireless
        {
            this.dpi = Convert.ToInt32(param[5]);
            this.wireless = Convert.ToBoolean(param[6]);
        }
        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}|Dpi: {this.dpi}|Wireless: {this.wireless}";
        }
        public new static string[] Prompt()
        {
            string[] param = new string[2];

            param[0] = getUserInput("dpi", [], "");
            param[1] = getUserInput("wireless", ["true", "false"], "");

            return param;
        } 

    }

    class Keyboard : Component
    {
        public string switchType { get; set; }
        public bool backlighting { get; set; }
        public bool wireless { get; set; }
        
        public Keyboard() { }
        public Keyboard(string[] param) : base(param) //param: type, name, price, desc, imgSrc, switchType, backlighting, wireless
        {
            this.switchType = param[5];
            this.backlighting = Convert.ToBoolean(param[6]);
            this.wireless= Convert.ToBoolean(param[7]);
        }
        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}|Switch type: {this.switchType}|Backlighting: {this.backlighting}|Wireless: {this.wireless}";
        }
        public new static string[] Prompt()
        {
            string[] param = new string[3];

            param[0] = getUserInput("switch type", [], "");
            param[1] = getUserInput("backlighting", ["true", "false"], "");
            param[2] = getUserInput("wireless", ["true", "false"], "");

            return param;
        } 
    }

    class Monitor : Component
    {
        public string size { get; set; }
        public string resolution { get; set; }
        public string refreshRate { get; set; }
        public string panelType { get; set; }
        public string responseTime { get; set; }

        public Monitor() { }
        public Monitor(string[] param) : base(param) //param: type, name, price, desc, imgSrc, size, resolution, refreshRate, panetType, responseTime
        {
            this.size = param[5];
            this.resolution = param[6];
            this.refreshRate = param[7];
            this.panelType = param[8];
            this.responseTime = param[9];
        }
        public string Stringify()
        {
            return $"Name: {this.title}|Type: {this.type}|Price: {this.price}|Size: {this.size}|Resolution: {this.resolution}|Refresh rate: {this.refreshRate}|Panel type: {this.panelType}|Response time: {this.responseTime}";
        }
        public new static string[] Prompt()
        {
            string[] param = new string[5];

            param[0] = getUserInput("size", [], "with units");
            param[1] = getUserInput("resolution", [], "e.g., 1920x1080");
            param[2] = getUserInput("refresh rate", [], "with units");
            param[3] = getUserInput("panel type", [], "");
            param[4] = getUserInput("response time", [], "with units");

            return param;
        } 
    }

    private static void Main(string[] args)
    {
        string fileName = "shop.json";

        Shop shop = new Shop();
        shop.ReadFromJson(fileName);


        while (true)
        {
            Console.WriteLine(
                $"[1] - Add component\n" +
                $"[2] - Search components\n" +
                $"[3] - Apply discount\n" +
                $"[4] - Save changes\n" +
                $"[5] - Exit");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine();
                    shop.AddComponent();
                    break;

                case "2":
                    shop.Search();
                    break;

                case "3":
                    shop.Discount();
                    break;

                case "4":
                    shop.WriteToJson(fileName);
                    break;

                case "5":
                    return;
            }

        }

    }
}
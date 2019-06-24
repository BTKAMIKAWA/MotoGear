namespace Service.Template
{
    public class App
    {
        private IModule[] _modules;

        public void Start()
        {
            _modules = new IModule[]
            {
  
            };
            foreach (var module in _modules)
            {
                module.Start();
            }
        }

        public void Stop()
        {
            foreach (var module in _modules)
            {
                module.Stop();
            }
        }
    }
}
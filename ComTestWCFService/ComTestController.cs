using ComTestWCFService.AlarmStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace ComTestWCFService
{
    public class ComTestController 
    {
        private ServiceHost _comTestHost;

        public ComTestController()
        {
            try
            {
                _comTestHost = new ServiceHost(typeof(ComTestManagement));
            }
            catch (Exception ep)
            {
                string error = ep.Message;
            }
        }

        public void Run()
        {
            _comTestHost.Open();
        }

        public void Stop()
        {

            _comTestHost.Close();


        }

    }
}
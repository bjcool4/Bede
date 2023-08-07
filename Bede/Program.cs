using Bede.Logic;
using Bede.Logic.Class;
using Bede.Logic.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace SlotMachineGame
{
    class Program
    {

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<ISlotMachineService, SlotMachineService>();
            var serviceProvider = services.BuildServiceProvider();

            var slotMachineService = serviceProvider.GetRequiredService<ISlotMachineService>();
            slotMachineService.Play();
        }
    }
}

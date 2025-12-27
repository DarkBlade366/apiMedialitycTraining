using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Queries
{
    //Vacio pq no pido nada, es un reporte global
    public class InventoryReportQuery 
        : ICommand<InventoryReportResponseDto>
    {
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Utilities;
using System.Globalization;

namespace Application.Prestamos.ObtenerCalculoCuotas;

public class ObtenerCalculoCuotasHandler : IRequestHandler<ObtenerCalculoCuotasRequest, ObtenerCalculoCuotasResponse>
{
    private readonly BaseContext _context;

    public ObtenerCalculoCuotasHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<ObtenerCalculoCuotasResponse> Handle(ObtenerCalculoCuotasRequest request, CancellationToken cancellationToken)
    {
        DateTime FechaCuota = Convert.ToDateTime(request.FechaInicio);
        decimal ValorCuota = request.ValorTotal / request.NoCuotas;
        string CadenaCuota = ValorCuota.ToString();
        int vc = 0, ind = 0;
        switch (CadenaCuota.Length)
        {
            case 4: vc = Convert.ToInt32(CadenaCuota.Substring(0, 1) + "000"); break;
            case 5: vc = Convert.ToInt32(CadenaCuota.Substring(0, 2) + "000"); break;
            case 6: vc = Convert.ToInt32(CadenaCuota.Substring(0, 3) + "000"); break;
            case 7: vc = Convert.ToInt32(CadenaCuota.Substring(0, 4) + "000"); break;
            case 8: vc = Convert.ToInt32(CadenaCuota.Substring(0, 5) + "000"); break;
        }
        decimal temp = request.ValorTotal - (vc * request.NoCuotas);
        decimal[] v = ObtenerGanancias(request.ValorTotal - request.ValorPrestamo, request.NoCuotas);
        List<ObtenerCalculoCuotasDTO> Lista = new();
        if (request.PeriodoCod.Equals(Constants.CodigoPeriodo_PorAbonos))
        {
            DateTime[] ListaDias = _context.DiaNoHabil.Where(x => x.UsuarioId == request.UsuarioId).Select(x => x.FechaDiaNoHabil).ToArray();
            for (int i = ind; i < request.NoCuotas; i++)
            {
                ObtenerCalculoCuotasDTO obCuota = new()
                {
                    FechaCuota = FechaCuota,
                    sFechaCuota = FechaCuota.ToString("dddd, dd 'de' MMMM 'de' yyyy", new CultureInfo("es-CO")),
                    Capital = vc - v[i],
                    Intereses = v[i],
                    ValorTotal = vc
                };
                Lista.Add(obCuota);
                FechaCuota = FechaCuota.AddDays(1);
                while (FechaCuota.DayOfWeek == DayOfWeek.Sunday || ListaDias.Contains(FechaCuota))
                {
                    FechaCuota = FechaCuota.AddDays(1);
                }
            }

            while (temp > 0)
            {
                for (int i = request.NoCuotas - 1; i >= 0; i--)
                {
                    if (temp > 1000) { Lista[i].Capital += 1000; temp -= 1000; }
                    else { Lista[i].Capital += temp; temp = 0; break; }
                }
            }
        }
        else
        {
            if (temp > 0)
            {
                ObtenerCalculoCuotasDTO obCuota = new()
                {
                    FechaCuota = Convert.ToDateTime(request.FechaInicio),
                    sFechaCuota = Convert.ToDateTime(request.FechaInicio).ToString("dddd, dd 'de' MMMM 'de' yyyy", new CultureInfo("es-CO")),
                    Capital = vc + temp - v[0],
                    Intereses = v[0],
                    ValorTotal = vc + temp
                };
                Lista.Add(obCuota);
                if (request.PeriodoCod.Equals(Constants.CodigoPeriodo_Quincenal)) { FechaCuota = ObtenerFechaQuincenal(FechaCuota); }
                else
                {
                    FechaCuota = request.PeriodoCod.Equals(Constants.CodigoPeriodo_Semanal) ? FechaCuota.AddDays(7) : request.PeriodoCod.Equals(Constants.CodigoPeriodo_Mensual) ? FechaCuota.AddMonths(1) : FechaCuota.AddDays(1);
                }
                ind = 1;
            }

            for (int i = ind; i < request.NoCuotas; i++)
            {
                ObtenerCalculoCuotasDTO obPrestDeta = new()
                {
                    FechaCuota = FechaCuota,
                    sFechaCuota = FechaCuota.ToString("dddd, dd 'de' MMMM 'de' yyyy", new CultureInfo("es-CO")),
                    Capital = vc - v[i],
                    Intereses = v[i],
                    ValorTotal = vc
                };
                Lista.Add(obPrestDeta);
                if (request.PeriodoCod.Equals(Constants.CodigoPeriodo_Quincenal)) { FechaCuota = ObtenerFechaQuincenal(FechaCuota); }
                else
                {
                    FechaCuota = request.PeriodoCod.Equals(Constants.CodigoPeriodo_Semanal) ? FechaCuota.AddDays(7) : request.PeriodoCod.Equals(Constants.CodigoPeriodo_Mensual) ? FechaCuota.AddMonths(1) : FechaCuota.AddDays(1);
                }
            }
        }

        decimal CapitalTotal = Lista.Sum(x => x.Capital);
        decimal InteresesTotal = Lista.Sum(x => x.Intereses);
        ObtenerCalculoCuotasResponse Resultado = new()
        {
            CapitalTotal = CapitalTotal,
            InteresesTotal = InteresesTotal,
            ValorTotal = CapitalTotal + InteresesTotal,
            ListadoCuotas = Lista
        };

        return Resultado;
    }


    private decimal[] ObtenerGanancias(decimal ganancia, int nocuotas)
    {
        decimal[] v = new decimal[nocuotas];
        decimal gan = ganancia / nocuotas;
        string cadenaganancia = gan.ToString();
        switch (cadenaganancia.Length)
        {
            case 3: gan = Convert.ToInt32(cadenaganancia.Substring(0, 1) + "00"); break;
            case 4: gan = Convert.ToInt32(cadenaganancia.Substring(0, 1) + "000"); break;
            case 5: gan = Convert.ToInt32(cadenaganancia.Substring(0, 2) + "000"); break;
            case 6: gan = Convert.ToInt32(cadenaganancia.Substring(0, 3) + "000"); break;
            case 7: gan = Convert.ToInt32(cadenaganancia.Substring(0, 4) + "000"); break;
            case 8: gan = Convert.ToInt32(cadenaganancia.Substring(0, 5) + "000"); break;
        }
        decimal temp = gan * nocuotas;
        if ((cadenaganancia.Length == 4 || cadenaganancia.Length == 3) && !cadenaganancia.Substring(1, 1).Equals("0"))
        {
            for (int i = 0; i < nocuotas; i++)
            {
                v[i] = gan;
            }
            temp = ganancia - temp;

            int restante = cadenaganancia.Length == 4 ? 1000 : 100;
            while (temp > 0)
            {
                for (int i = nocuotas - 1; i >= 0; i--)
                {
                    if (temp > restante) { v[i] = v[i] + restante; temp -= restante; }
                    else { v[i] = v[i] + temp; temp = 0; break; }
                }
            }
        }
        else
        {
            int ind = 0;
            temp = ganancia - temp;
            if (temp > 0)
            {
                ind = 1;
                v[0] = gan + temp;
            }
            for (int i = ind; i < nocuotas; i++)
            {
                v[i] = gan;
            }
        }
        return v;
    }

    private DateTime ObtenerFechaQuincenal(DateTime fecha)
    {
        DateTime nuevafecha = fecha.AddDays(14);
        switch (fecha.Day)
        {
            case 5: nuevafecha = fecha.AddDays(15); break;
            case 20: nuevafecha = Convert.ToDateTime("05/" + fecha.AddMonths(1).Month + "/" + fecha.AddMonths(1).Year); break;
            case 28:
            case 29:
                {
                    if (fecha.Month == 2)
                    {
                        nuevafecha = Convert.ToDateTime("15/03/" + fecha.Year);
                    }
                    break;
                }
            case 15:
                {
                    if (fecha.Month != 2)
                    {
                        string? mes = Constants.Meses31.FirstOrDefault(m => m == fecha.Month.ToString());
                        nuevafecha = string.IsNullOrEmpty(mes) ? fecha.AddDays(15) : fecha.AddDays(16);
                    }
                    else
                    {
                        nuevafecha = (fecha.Year % 4 == 0 && (fecha.Year % 100 != 0 || fecha.Year % 400 == 0)) ? fecha.AddDays(14) : fecha.AddDays(13);
                    }
                    break;
                }
            case 30:
            case 31:
                {
                    nuevafecha = Convert.ToDateTime("15/" + fecha.AddMonths(1).Month + "/" + fecha.AddMonths(1).Year);
                    break;
                }
        }
        return nuevafecha;
    }
}

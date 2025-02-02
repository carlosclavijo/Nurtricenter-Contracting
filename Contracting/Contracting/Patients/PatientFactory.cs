using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Contracting.Domain.Shared;

namespace Contracting.Domain.Patients;

public class PatienteFactory : IPatienteFactory
{
    public Patient Create(string patientName, string patientPhone)
    {
        if (string.IsNullOrWhiteSpace(patientName))
        {
            throw new ArgumentNullException("Patient name is required", nameof(patientName));
        }
        if (string.IsNullOrWhiteSpace(patientPhone))
        {
            throw new ArgumentNullException("Patient name is required", nameof(patientName));
        }
        /*if (Regex.IsMatch(patientPhone, @"^\d{8}$"))
        {
            throw new ArgumentException("Patient number needs to be 8 size length", nameof(patientPhone));
        }*/
        return new Patient(patientName, patientPhone);
    }
}
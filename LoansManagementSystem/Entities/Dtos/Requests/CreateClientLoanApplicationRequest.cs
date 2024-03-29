﻿namespace LoansManagementSystem.Entities.Dtos.Requests;

public class CreateClientLoanApplicationRequest
{
    public string EmploymentType { get; set; } = null!;
    public int MonthIncome { get; set; }
    public int Term { get; set; }
    public int Amount { get; set; }
    public string Purpose { get; set; } = null!;
}
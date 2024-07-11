using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using System;
using System.Collections.Generic;

namespace MediBook.ViewModels.Default
{
  public class PatientPageViewModel
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string About { get; set; }
    public int CurrentUserId { get; set; }
    public CalendarViewModel Calendar { get; set; }
    public IEnumerable<Shared.ConsultationViewModel> Consultations { get; set; }
  }
}
using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace MediBook.Actions
{
	public class UseMvcAction : IUseEndpointsAction	
  {
		public int Priority => 4000;

		public void Execute(IEndpointRouteBuilder routeBuilder, IServiceProvider serviceProvider)
		{
			// Account
			routeBuilder.MapControllerRoute(name: "Sign In", pattern: "sign-in", defaults: new { controller = "Account", action = "SignIn" });
			routeBuilder.MapControllerRoute(name: "Sign Up", pattern: "sign-up", defaults: new { controller = "Account", action = "SignUp" });
			routeBuilder.MapControllerRoute(name: "Sign Out", pattern: "sign-out", defaults: new { controller = "Account", action = "SignOut" });
			routeBuilder.MapControllerRoute(name: "Confirm Email", pattern: "confirm-email", defaults: new { Controller = "Account", action = "ConfirmEmail" });
			routeBuilder.MapControllerRoute(name: "Generate Restore Password", pattern: "restore-password", defaults: new { Controller = "Account", action = "GenerateResetPasswordToken" });
			routeBuilder.MapControllerRoute(name: "Restore Password", pattern: "restore-password/confirm", defaults: new { Controller = "Account", action = "RestorePassword" });
			routeBuilder.MapControllerRoute(name: "About", pattern: "about", defaults: new { controller = "Account", action = "About" });

      // Doctors
      routeBuilder.MapControllerRoute(name: "Doctors List", pattern: "doctors", defaults: new { controller = "Doctors", action = "Doctors" });
			routeBuilder.MapControllerRoute(name: "Current Doctor Page", pattern: "doctors/me", defaults: new { controller = "Doctors", action = "CurrentDoctorPage" });
			routeBuilder.MapControllerRoute(name: "Current Doctor Consultations", pattern: "doctors/me/consultations", defaults: new { controller = "Doctors", action = "CurrentDoctorConsultations" });
			routeBuilder.MapControllerRoute(name: "Doctor Page", pattern: "doctors/{id}", defaults: new { controller = "Doctors", action = "DoctorPage" });
			routeBuilder.MapControllerRoute(name: "Doctors Edit", pattern: "doctors/me/edit", defaults: new { controller = "Doctors", action = "Edit" });
			routeBuilder.MapControllerRoute(name: "Doctors Shedule", pattern: "doctors/me/schedule/{year}/{month}/{day}", defaults: new { controller = "Doctors", action = "Schedule" });
			routeBuilder.MapControllerRoute(name: "Doctors Cancel consultation", pattern: "doctors/me/consultations/{id}/cancel", defaults: new { controller = "Doctors", action = "CancelConsultation" });
			routeBuilder.MapControllerRoute(name: "Doctors Complete consultation", pattern: "doctors/me/consultations/{id}/complete", defaults: new { controller = "Doctors", action = "CompleteSupervision" });
			routeBuilder.MapControllerRoute(name: "Doctors Schedule", pattern: "doctors/{id}/book/{year}/{month}/{day}", defaults: new { controller = "Doctors", action = "SchedulePatient" });

      // Patients
      routeBuilder.MapControllerRoute(name: "Current patient", pattern: "patients/me", defaults: new { controller = "Patients", action = "MyPage" });
			routeBuilder.MapControllerRoute(name: "Patient Page", pattern: "patients/{id}", defaults: new { controller = "Patients", action = "PatientPage" });
			routeBuilder.MapControllerRoute(name: "Patient Edit", pattern: "patients/me/edit", defaults: new { controller = "Patients", action = "Edit" });
			routeBuilder.MapControllerRoute(name: "Patient consultations", pattern: "patients/me/consultations", defaults: new { controller = "Patients", action = "MyConsultations" });
      routeBuilder.MapControllerRoute(name: "Doctors Cancel consultation", pattern: "patients/me/consultations/{id}/cancel", defaults: new { controller = "Patients", action = "CancelConsultation" });
      routeBuilder.MapControllerRoute(name: "Patient Shedule", pattern: "patients/me/schedule/{year}/{month}/{day}", defaults: new { controller = "Patients", action = "Schedule" });

      // Consultations
      routeBuilder.MapControllerRoute(name: "Consultation Page", pattern: "consultations/{id}", defaults: new { controller = "Consultations", action = "Consultation" });
      routeBuilder.MapControllerRoute(name: "Patient Did Not Show Up", pattern: "consultations/{id}/mark-canceled", defaults: new { controller = "Consultations", action = "MarkAsCanceled" });
      routeBuilder.MapControllerRoute(name: "Add Attachment", pattern: "consultations/{id}/attachment", defaults: new { controller = "Consultations", action = "AddAttachment" });
      routeBuilder.MapControllerRoute(name: "Add Link", pattern: "consultations/{id}/add-link", defaults: new { controller = "Consultations", action = "AddLink" });
			routeBuilder.MapControllerRoute(name: "Consultation Book", pattern: "consultations/{id}/book", defaults: new { controller = "Doctors", action = "Book" });

      // Calendar
      routeBuilder.MapControllerRoute(name: "Supervisor Calendar", pattern: "calendar/doctor/{id}", defaults: new { controller = "Calendar", action = "ForDoctor" });
			routeBuilder.MapControllerRoute(name: "Supervisee Calendar", pattern: "calendar/patient/{id}", defaults: new { controller = "Calendar", action = "ForPatient" });
		}
	}
}
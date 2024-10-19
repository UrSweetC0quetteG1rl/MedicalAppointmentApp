using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointment.Persistance.Models;
using MedicalAppointmentApp.Domain.Entities.Medical;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.Configuration.Medical
{
    public class MedicalRecordRepository(MedicalAppointmentContext RecordContext, ILogger<MedicalRecordRepository> logger)
        : BaseRepository<MedicalRecords>(RecordContext), IMedicalRecordsRepository

    {


        public readonly MedicalAppointmentContext RecordContext = RecordContext;
        public readonly ILogger<MedicalRecordRepository> logger = logger;


        public async override Task<OperationResult> Save(MedicalRecords entity)
        {

            OperationResult operationResult = new OperationResult();


            operationResult = ValidarMedicalRecord(entity);
            if (!operationResult.Success) return operationResult;

            if (await base.Exists(MedicalRecords => MedicalRecords.DoctorID == entity.DoctorID && MedicalRecords.PatientID == entity.PatientID && MedicalRecords.Diagnosis == entity.Diagnosis
            && MedicalRecords.Treatment == entity.Treatment && MedicalRecords.DateOfVisit == entity.DateOfVisit))
            {

                operationResult.Success = false;
                operationResult.Message = "Ya existe.";
                return operationResult;


            }


            try
            {


                operationResult = await base.Save(entity);

            }
            catch (Exception ex)
            {


                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al guardar.";
                this.logger.LogError(operationResult.Message, ex.ToString());



                return operationResult;

            }
            return operationResult;




        }

        public async override Task<OperationResult> Update(MedicalRecords entity)
        {
            OperationResult operationResult = new OperationResult();


            operationResult = ValidarMedicalRecord(entity);
            if (!operationResult.Success) return operationResult;

            try
            {


                MedicalRecords? MedicalRecordToUpdate = await RecordContext.MedicalRecords.FindAsync(entity.RecordID);

                if (MedicalRecordToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }


                MedicalRecordToUpdate.Treatment = entity.Treatment;
                MedicalRecordToUpdate.PatientID = entity.PatientID;
                MedicalRecordToUpdate.Diagnosis = entity.Diagnosis;
                MedicalRecordToUpdate.UpdatedAt = DateTime.Now;
                MedicalRecordToUpdate.DateOfVisit = entity.DateOfVisit;



                operationResult = await base.Update(MedicalRecordToUpdate);


            }

            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al Reprogramar la cita.";
                logger.LogError(operationResult.Message, args: ex.ToString);

            }

            return operationResult;


        }


        public async override Task<OperationResult> Remove(MedicalRecords entity)
        {
            OperationResult operationResult = new OperationResult();


            operationResult = ValidarMedicalRecord(entity);
            if (!operationResult.Success) return operationResult;

            try
            {


                MedicalRecords? MedicalRecordToRemove = await RecordContext.MedicalRecords.FindAsync(entity.RecordID);

                if (MedicalRecordToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }

                MedicalRecordToRemove.IsActive = false;
                MedicalRecordToRemove.UpdatedAt = DateTime.Now; 
                

                


                operationResult = await base.Update(MedicalRecordToRemove);


            }

            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al Reprogramar la cita.";
                logger.LogError(operationResult.Message, args: ex.ToString);

            }

            return operationResult;
        }



        public async override Task<OperationResult> GetAll()
        {
            

            OperationResult operationResult = new OperationResult();    


            try
            {

                operationResult.Data = await (from MedicalRecords in RecordContext.MedicalRecords join doctor in RecordContext.Doctors on MedicalRecords.DoctorID equals doctor.DoctorID
                                              join patient in RecordContext.Patients on MedicalRecords.PatientID equals patient.PatientID
                                              where MedicalRecords.IsActive == true orderby MedicalRecords.CreatedAt descending
                                              select new MedicalRecordsModel
                                              {

                                                  RecordID = MedicalRecords.RecordID,
                                                  DoctorID = MedicalRecords.DoctorID,
                                                  Diagnosis = MedicalRecords.Diagnosis,
                                                  Treatment = MedicalRecords.Treatment,
                                                  CreatedAt = MedicalRecords.CreatedAt,
                                                  DateOfVisit = MedicalRecords.DateOfVisit,
                                                  PatientID = MedicalRecords.PatientID,
                                                  

                                              }).ToListAsync(); ;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo los asientos";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;


        }

        private OperationResult ValidarMedicalRecord(MedicalRecords entity)
        {
            OperationResult operationResult = new OperationResult();



            if (entity == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Esta entidad no Puede ser Nula.";
                return operationResult;


            }


            if (entity.PatientID <= 0)
            {

                operationResult.Success = false;
                operationResult.Message = "Debe Ingresar el paciente.";
                return operationResult;


            }

            if (entity.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "Debe seleccionar el Doctor.";
                return operationResult;

            }

            if (string.IsNullOrEmpty(entity.Treatment))
            {
                operationResult.Success = false;
                operationResult.Message = "Debe Ingresar el tratamiento.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.Diagnosis))
            {
                operationResult.Success = false;
                operationResult.Message = "Debe Ingresar un Diagnostico.";
                return operationResult;
            }

            if (entity.DateOfVisit == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Debe Ingresar una Fecha.";
                return operationResult;
            }




            operationResult.Success = true;
            return operationResult;

        }


    }
}







using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointment.Persistance.Models;
using MedicalAppointment.Persistance.Models.Medical;
using MedicalAppointmentApp.Domain.Entities.Medical;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MedicalAppointment.Persistance.Repositories.Configuration.Medical
{
    public class SpecialtiesRepository (MedicalAppointmentContext specialtiesContext, ILogger<SpecialtiesRepository> logger)
        : BaseRepository<Specialties> (specialtiesContext), ISpecialtiesRepository
    {

        public readonly MedicalAppointmentContext SpecialtiesContext = specialtiesContext;
        public readonly ILogger<SpecialtiesRepository> logger = logger;


        public async override Task<OperationResult> Save(Specialties entity)
        {

            OperationResult operationResult = new OperationResult();


            operationResult = ValidarSpecialty(entity);
            if (!operationResult.Success) return operationResult;

           /* if (await base.Exists(specialty => specialty.SpecialtyID == entity.SpecialtyID && specialty.SpecialtyName == entity.SpecialtyName))
            {

                operationResult.Success = false;
                operationResult.Message = "Ya existe esa especialidad";
                return operationResult;


            }*/


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


        public async override Task<OperationResult> Update(Specialties entity)
        {
            OperationResult operationResult = new OperationResult();


            operationResult = ValidarSpecialty(entity);
            if (!operationResult.Success) return operationResult;

            try
            {


                Specialties? SpecialtiesToUpdate = await SpecialtiesContext.Specialties.FindAsync(entity.SpecialtyID);

                if (SpecialtiesToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }

                SpecialtiesToUpdate.SpecialtyName = entity.SpecialtyName;
                SpecialtiesToUpdate.UpdatedAt = DateTime.Now;
                SpecialtiesToUpdate.IsActive = entity.IsActive;

                



                operationResult = await base.Update(SpecialtiesToUpdate);


            }

            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al Reprogramar la cita.";
                logger.LogError(operationResult.Message, args: ex.ToString);

            }

            return operationResult;
        }


        public async override Task<OperationResult> Remove(Specialties entity)
        {
            OperationResult operationResult = new OperationResult();


            operationResult = ValidarSpecialty(entity);
            if (!operationResult.Success) return operationResult;

            try
            {


                Specialties? SpecialtiesToRemove = await SpecialtiesContext.Specialties.FindAsync(entity.SpecialtyID);


                if (SpecialtiesToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }

                SpecialtiesToRemove.IsActive = false;
                SpecialtiesToRemove.UpdatedAt = DateTime.Now;





                operationResult = await base.Update(SpecialtiesToRemove);


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

                operationResult.Data = await (from Specialties in SpecialtiesContext.Specialties
                                             
                                              where Specialties.IsActive == true
                                              orderby Specialties.CreatedAt descending
                                              select new SpecialtiesModel
                                              {

                                                  SpecialtyID = Specialties.SpecialtyID,
                                                  SpecialtyName = Specialties.SpecialtyName,
                                                  IsActive = Specialties.IsActive,
                                                  UpdatedAt = Specialties.UpdatedAt


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

        private OperationResult ValidarSpecialty(Specialties entity)
        {
            OperationResult operationresult = new OperationResult();

            if (entity == null)
            {
                operationresult.Success = false;
                operationresult.Message = "Esta entidad no puede ser nula.";
                return operationresult;
            }

            if (string.IsNullOrEmpty(entity.SpecialtyName))
            {
                operationresult.Success = false;
                operationresult.Message = "Debe agregar nombre de la especialidad";
                return operationresult;
            }


            operationresult.Success = true;
            return operationresult;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JetRx.Entities;
using JetRx.Common.Data.SqlServer;

namespace JetRx.Common.Services
{
    public class CustomerService
    {
        public prescription SaveOrder(prescription Prescription, string ImageName, JetRxSession userSession)
        {
            using (var context = new JetRxContext())
            {
                var p = context.CustomerPrescriptions.FirstOrDefault(c => c.Prescription.id == Prescription.id && c.Customer.id == userSession.Customer.id);
                
             
                if (p == null)
                {
                    var _customerPrescription = new CustomerPrescription {
                        Prescription = Prescription,
                        Customer = userSession.Customer,
                        Device = userSession.Device,
                        created_at = DateTime.UtcNow,

                    };
                    context.CustomerPrescriptions.Add(_customerPrescription);
                    context.SaveChanges();

                    context.Orders.Add(new order {
                         customer_prescription = _customerPrescription,
                         status = context.OrderStatus.FirstOrDefault(s=>s.stepOrder ==0),
                          created_at = DateTime.UtcNow
                    });

                 
                }
                else
                {
                    p.Prescription.doctor_address = Prescription.doctor_address;
                    p.Prescription.doctor_name = Prescription.doctor_name;
                    p.Prescription.doctor_phonenumber = Prescription.doctor_phonenumber;
                    p.Prescription.barcode = Prescription.barcode;
                    p.Prescription.duration = Prescription.duration;
                    p.Prescription.prescribed_date = Prescription.prescribed_date;
                    p.Prescription.quantity = Prescription.quantity;
                    p.Prescription.refill = Prescription.refill;
                    p.Prescription.prescription_product = Prescription.prescription_product;
                    p.updated_at = DateTime.UtcNow;
                   
                  
                }
               

                if (!string.IsNullOrEmpty(ImageName))
                {

                    var Image = context.Images.FirstOrDefault(i => i.owner_id == Prescription.id);

                    if (Image == null)
                    {
                        Image= new image();
                        Image.owner_id = Prescription.id;
                        Image.image_type = imagetype.Prescription;
                        Image.url = ImageName;

                        context.Images.Add(Image);
                        context.SaveChanges();
                    }
                    else
                        Image.url = ImageName;

                    Prescription.image_id = Image.id;
                }


                context.SaveChanges();

                return Prescription;
            }
        }

        

        public prescription GetPrescription( int prescriptionId)
        {
            using (var context = new JetRxContext())
            {
                return context.Prescriptions.FirstOrDefault(p => p.id == prescriptionId);
            }
        }

        public List<prescription> GetPrescriptions(int customerId)
        {
            using (var context = new JetRxContext())
            {
                var prescriptions = from cp in context.CustomerPrescriptions
                                        where cp.Customer.id == customerId
                                    select cp.Prescription;

                return prescriptions.ToList();
            }
        }

        public identification AddCustomerIdentification(identification Identification, string ImageName, JetRxSession userSession)
        {
            using (var context = new JetRxContext())
            {
                var customer = context.Customers.FirstOrDefault(c => c.id == userSession.Customer.id);
                customer.Identification = Identification;
                context.SaveChanges();

                image Image = new image();
                Image.owner_id = Identification.id;
                Image.image_type = imagetype.Identification;
                Image.url = ImageName;

                context.Images.Add(Image);
                context.SaveChanges();

                Identification.image_id = Image.id;

                //context.CustomerIdentifications.Add(new CustomerIdentification
                //{
                //    CustomerId = CustomerId,
                //    DeviceId = DeviceId,
                //    IdentificationId = Identification.id,
                //    created_at = DateTime.UtcNow
                //});

                //context.SaveChanges();

                return Identification;
            }
        }

        public insurance AddCustomerInsurance(insurance Insurance, string ImageName, JetRxSession userSession)
        {
            using (var context = new JetRxContext())
            {
                var customer = context.Customers.FirstOrDefault(c => c.id == userSession.Customer.id);
                customer.Insurance = Insurance;
                context.SaveChanges();

                image Image = new image();
                Image.owner_id = Insurance.id;
                Image.image_type = imagetype.Identification;
                Image.url = ImageName;

                context.Images.Add(Image);
                context.SaveChanges();

                Insurance.image_id = Image.id;

                //context.CustomerInsuranceDetails.Add(new CustomerInsurance
                //{
                //    CustomerId = CustomerId,
                //    DeviceId = DeviceId,
                //    InsuranceId = Insurance.id,
                //    created_at = DateTime.UtcNow
                //});

                context.SaveChanges();

                return Insurance;
            }
        }
    }
}

using Entities.Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class ScheduleRepository : IDisposable
    {
        public List<ScheduleTask> ScheduleTask(ScheduleTaskRequestParam value)
        {
            List<ScheduleTask> scheduleTasks = new List<ScheduleTask>();
            DateTime startDate = new DateTime();
            List<Employee> employees = new List<Employee>();
            int counter = 1;
            try
            {
                if (value.NumberOfEmployee > 0)
                {
                    for (int i = 0; i < value.NumberOfEmployee; i++)
                    {
                        Employee employee = new Employee();
                        employee.Id = i + 1;
                        employee.Name = "Engineer " + employee.Id;
                        employees.Add(employee);
                    }
                }

                if (value.DateRange != null && value.DateRange.Length == 2)
                {
                    int days = (int)(value.DateRange[1] - value.DateRange[0]).TotalDays + 1;
                    if (days >= employees.Count)
                    {
                        startDate = value.DateRange[0];
                        for (int i = 0; i < days; i++)
                        {
                            ScheduleTask objST1 = new ScheduleTask();
                            ScheduleTask objST2 = new ScheduleTask();

                            //break loop if employees count is even and index i == employees count
                            if (i == employees.Count && employees.Count % 2 == 0)
                            {
                                break;
                            }
                            //break loop if employees count is odd and index i == employees count + 1
                            else if (i == employees.Count + 1 && employees.Count % 2 != 0)
                            {
                                break;
                            }

                            //check dayOfWeek of date is Sunday or not
                            if (startDate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                //check whether selected weekend is SaturdayAndSunday or Sunday only
                                if (value.Weekend == (int)Weekend.SaturdayAndSunday || value.Weekend == (int)Weekend.Sunday)
                                {
                                    // add 1 day to date to skip saturday or sunday
                                    startDate = startDate.AddDays(1);
                                }
                            }
                            //check dayOfWeek of date is Sunday or not
                            else if (startDate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                //check whether selected weekend is SaturdayAndSunday
                                if (value.Weekend == (int)Weekend.SaturdayAndSunday)
                                {
                                    // add 2 day to date to skip saturday and sunday
                                    startDate = startDate.AddDays(2);
                                }
                            }

                            if (counter % 2 != 0)
                            {
                                objST1.id = employees[i].Id;
                                objST1.title = "Shift 1: " + employees[i].Name;

                                //add time span for shift 1 and format date to string
                                objST1.start = startDate.Add(new TimeSpan(0, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");
                                objST1.end = startDate.Add(new TimeSpan(12, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");

                                if ((employees.Count % 2 != 0 && i < employees.Count - 1) || employees.Count % 2 == 0)
                                {
                                    objST2.id = employees[i + 1].Id;
                                    objST2.title = "Shift 2: " + employees[i + 1].Name;

                                    //add time span for shift 2 and format date to string
                                    objST2.start = startDate.Add(new TimeSpan(12, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");
                                    objST2.end = startDate.Add(new TimeSpan(24, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");
                                }
                            }
                            else
                            {
                                if ((employees.Count % 2 != 0 && i < employees.Count - 1) || employees.Count % 2 == 0)
                                {
                                    objST1.id = employees[i].Id;
                                    objST1.title = "Shift 1: " + employees[i].Name;
                                    //add time span for shift 1 and format date to string
                                    objST1.start = startDate.Add(new TimeSpan(0, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");
                                    objST1.end = startDate.Add(new TimeSpan(12, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");
                                }

                                objST2.id = employees[i - 1].Id;
                                objST2.title = "Shift 2: " + employees[i - 1].Name;
                                //add time span for shift 2 and format date to string
                                objST2.start = startDate.Add(new TimeSpan(12, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");
                                objST2.end = startDate.Add(new TimeSpan(24, 0, 0)).ToString("yyyy-MM-dd'T'HH:mm:ss");
                            }

                            //after afternoon shift
                            counter++;
                            scheduleTasks.Add(objST1);
                            scheduleTasks.Add(objST2);
                            startDate = startDate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return scheduleTasks;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// Is this instance disposed?
        /// </summary>
        protected bool Disposed { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="disposing">Are we disposing? 
        /// Otherwise we're finalizing.</param>
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
}

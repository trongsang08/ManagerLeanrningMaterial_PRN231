using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MaterialDao
    {
        public static IEnumerable<Material> GetMaterialsByCourseId(int courseId)
        {
            List<Material> list = new List<Material>();
            try
            {
                
                using(var context = new Prn231ProjectContext())
                {
                    list= context.Materials.Include(m => m.Course)
                        .Include(m => m.Uploader)
                        .Where(m => m.CourseId== courseId).ToList();
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message); 
            }
            return list;
        }


        public static void SaveMaterial(IFormFile material,string materialPath,int courseId,int uploaderId,string MaterialName)
        {
            using(var context = new Prn231ProjectContext())
            {
                using( var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Material newMaterial= new Material
                        {
                            CourseId = courseId,
                            MaterialName = MaterialName,
                            Path= materialPath,
                            UploaderId = uploaderId
                        };
                        context.Materials.Add(newMaterial);
                        string ext = Path.GetExtension(materialPath);
                        string fileNameWithoutExtention = Path.GetFileNameWithoutExtension(materialPath);
                        newMaterial.MaterialName = fileNameWithoutExtention + "_" + Guid.NewGuid().ToString() + ext;
                        string materialPath2 = Path.Combine(materialPath, newMaterial.MaterialName);
                        while(File.Exists(materialPath2))
                        {
                            materialPath2 = Path.Combine(materialPath, fileNameWithoutExtention + "_" + Guid.NewGuid().ToString() + ext);
                        }
                        if(context.SaveChanges() > 0)
                        {
                            transaction.Commit();
                            if(File.Exists(materialPath2))
                            {
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                File.Delete(materialPath2);
                            }
                            material.CopyTo(new FileStream(materialPath2,FileMode.Create));
                        }
                    }
                    catch (Exception ex)
                    {
                       throw new Exception(ex.Message);
                    }
                }
            }
        }

        public static void DeleteMaterial(int materialId)
        {
            using(var context = new Prn231ProjectContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    var material = context.Materials.Where(m => m.MaterialId== materialId).FirstOrDefault();
                    context.Materials.Remove(material);
                    if(context.SaveChanges() > 0)
                    {
                        transaction.Commit();
                    }
                }
            }
        }

        public static Material GetMaterialById(int materialId)
        {
            Material material = new Material();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    material = context.Materials.Where(m => m.MaterialId == materialId).FirstOrDefault();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return material;
        }
    }
}

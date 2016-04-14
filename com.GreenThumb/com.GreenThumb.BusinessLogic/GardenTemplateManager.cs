using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Nicholas King
    /// </summary>
    public class GardenTemplateManager
    {
        public GardenTemplateManager()
        {

        }



        public bool SaveTemplate(string filePath, AccessToken at, string fileName)
        {
            bool result = false;

            try
            {
                byte[] file;
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var reader = new BinaryReader(stream);
                file = reader.ReadBytes((int)stream.Length);

                if (ExpertAccessor.CreateGardenTemplate(file, at.UserID, fileName) == 2)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public byte[] LoadTemplate(string fileName)
        {
            byte[] data = null;


            //put this in the load template page... pull png from database to memoryStream, then convert to BitmapImage. then put as image source
            data = ExpertAccessor.RetrieveGardenTemplate(fileName);


            return data;
        }

        public List<GardenTemplate> GetTemplateList()
        {
            return ExpertAccessor.RetrieveAllGardenTemplates();
        }

    }
}

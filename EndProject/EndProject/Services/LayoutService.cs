using EndProject.Models;
using EndProject.Services.Interfaces;

namespace EndProject.Services
{
    public class LayoutService : ILayoutService
    {
        public Setting GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<SectionHeader> GetSectionAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<SectionBackgroundImage> GetSectionBackgroundImageByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SectionBackgroundImage>> GetSectionBackgroundImageDatasAsync()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetSectionBackgroundImages()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SectionHeader>> GetSectionsDatasAsync()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetSectionsHeaders()
        {
            throw new NotImplementedException();
        }

        public Task<List<Setting>> GetSettingDatas()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetSettings()
        {
            throw new NotImplementedException();
        }
    }
}

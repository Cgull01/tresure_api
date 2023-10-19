using API_tresure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace tresure_api.Data.Interfaces
{
    public interface IColumnRepository
    {
        Task <ICollection<Column>> GetColumns();
        //Task <ICollection<Column>> GetColumnsByProjectId(int projectId);

        Task <Column> GetColumnById(int id);

        bool CreateColumn(Column column);
        bool UpdateColumn(Column column);
        bool DeleteColumn(Column column);
        bool Save();

    }
}

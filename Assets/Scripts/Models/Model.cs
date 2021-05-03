using System;

namespace PirateCave.Models
{
    /// <summary>
    /// Atributos padrões de todos as models
    /// </summary>
    public abstract class Model
    {
        public DateTime createdAt;
        public DateTime? updatedAt;
    }
}
using System;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Exceção específica de validação de regras de domínio.
    /// Sinaliza falhas de negócio que não são técnicas
    /// </summary>
    public class DomainValidationException : Exception
    {
        /// <summary>
        /// Construtor para receber a mensagem de erro.
        /// </summary>
        /// <param name="message">Descrição da falha de domínio.</param>
        public DomainValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Construtor para receber uma exceção interna.
        /// </summary>
        /// <param name="message">Descrição da falha de domínio.</param>
        /// <param name="innerException">Exceção filha, se houver.</param>
        public DomainValidationException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
using System.ComponentModel;

namespace SwaggerWebApiSample.V1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an order.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was created.
        /// </summary>
        /// <value>The order's creation date.</value>
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets or sets the name of the ordering customer.
        /// </summary>
        /// <value>The name of the customer that placed the order.</value>
        [Required]
        public string Customer { get; set; }
    }
}
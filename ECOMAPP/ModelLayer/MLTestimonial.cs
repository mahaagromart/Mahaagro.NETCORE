namespace ECOMAPP.ModelLayer
{
    public class MLTestimonial
    {
        public class Testimonial
        {

            public int Category_id { get; set; }
            public string Category_Name { get; set; }
            public string CreationDate { get; set; }
            public string Image { get; set; }
            public int Priority { get; set; }
            public string Status { get; set; }

        }
        public class MLInsertTestimonial
        {
            public int Category_id { get; set; }
            public string Category_Name { get; set; }
            public string CreationDate { get; set; }
            public string Image { get; set; }
            public int Priority { get; set; }

        }
        public class MLUpdateTestimonial
        {
            public int Category_id { get; set; }
            public string Category_Name { get; set; }
            public string CreationDate { get; set; }
            public string Image { get; set; }

        }
        public class MLDeleteTestimonial
        {
            public int Category_id { get; set; }

        }

        public List<MLTestimonial.Testimonial> TestimonialList { get; set; }


    }
}

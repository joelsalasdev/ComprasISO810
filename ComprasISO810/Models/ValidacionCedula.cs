namespace ComprasISO810.Models
{
    public class ValidacionCedula
    {
        public bool ValidateCedula(string value)
        {
            string cedula = value.Replace("-", "").Trim();
            return ValidaCedula(cedula);
        }

        private bool ValidaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula;
            int pLongCed = vcCedula.Length;
            int[] digito_mult = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed != 11)
            {
                return false;
            }

            for (int vDig = 0; vDig < 11; vDig++)
            {
                int vCalculo = int.Parse(vcCedula[vDig].ToString()) * digito_mult[vDig];
                if (vCalculo < 10)
                {
                    vnTotal += vCalculo;
                }
                else
                {
                    vnTotal += int.Parse(vCalculo.ToString()[0].ToString()) + int.Parse(vCalculo.ToString()[1].ToString());
                }
            }

            return vnTotal % 10 == 0;
        }
    }
}

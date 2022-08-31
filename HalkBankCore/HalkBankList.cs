using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalkBankCore
{
    public class HalkBankList : IBankList, IEnumerable
    {
        private object[] _dizi;

        public HalkBankList()
        {
            _dizi = new object[0];
        }

        public object this[int index]
        {
            get => _dizi[index];
            set => _dizi[index] = value;
        }

        public int ElemanSayisi => _dizi.Length;

        public void Ekle(object eleman)
        {
            var yedekDizi = _dizi;
            _dizi = new object[ElemanSayisi + 1];

            yedekDizi.CopyTo(_dizi, 0);

            _dizi[ElemanSayisi-1] = eleman;
        }

        //MoveNext();Reset();Current
        public IEnumerator GetEnumerator()
        {

            IEnumerator myEnumerator = _dizi.GetEnumerator();

            while (myEnumerator.MoveNext() && (myEnumerator.Current != null))
                yield return myEnumerator.Current;

            /*
            Compiler yield anahtar sözcüğünü gördüğü anda bu keywordün
            bulunduğu bloğun bir iterator bloğu olduğunu algılamaktadır.
            Bu adımdan sonra foreach döngüsü içerisinde ilgili koleksiyonu
            dönen metod çağrıldıktan sonra metod içerisindeki bir yield return
            ifadesine gelinince değer geriye dönülmekte ve bu işlem yapılmadan
            önce Compiler, iterator metodun kaldığı yeri saklamaktadır.
            Süreç esnasında foreach döngüsünde iterator metoduna gelen her
            istekte iterator metot baştan başlamak yerine kaldığı yerden işletilmektedir.
            */
        }

        public void KosulluSil(Func<object, bool> kosul)
        {
            for (int index = 0; index < _dizi.Length; index++)
            {
                if (kosul(_dizi[index]))
                {
                    Sil(index);
                }
            }
        }

        public void Sil(int index)
        {
            var yedekDizi = _dizi;
            _dizi = new object[ElemanSayisi - 1];

            int sayac = 0;

            for (int i = 0; i < yedekDizi.Length; i++)
            {
                if (i == index)
                    continue;

                _dizi[sayac] = yedekDizi[i];

                sayac++;
            }
        }

        public void Temizle()
        {
            _dizi = new object[0];
        }
    }
}

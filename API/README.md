version https://git-lfs.github.com/spec/v1
oid sha256:d3fc4d8c7018207a9459a6b46a4fba594b0aed18502dbca5407ac1942631c963
size 16
virualenv de çalıştırmak için 
//virtualenv kurmadan çalıştırmak çakışmalara sebep olmaktadır
```
source [virtualenv'ın oldugu klasör]/bin/activate // önce virtual env indirlimiş ve de bir tane env kurulmuş olmalı 
pip install -r requirements.txt
```

Admin Page için 
```
pyhton
from app import db
db.create_all()
exit()

//create migration
flask db init
flask db migrate -m "Initial migration"
flask db upgrade

```

virtual env kurmak için 
```
pip install virtualenv
mkdir pentaVR
cd pentaVR
virtualenv venvBitirme
source venvBitirme/bin/activate
```

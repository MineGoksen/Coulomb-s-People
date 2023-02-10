# Coulomb-s-People Admin Page API
Veri tabanına soru ve ipuçları için veri girişi sağlanacak sitedir.

# Çalıştırılma

```python
pyhton # windows cihazlar için py olabilir?
from app import db
db.create_all()
exit()
```
then

```
//create migration
flask db init
flask db migrate -m "Initial migration"
flask db upgrade
```

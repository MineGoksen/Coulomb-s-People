version https://git-lfs.github.com/spec/v1
oid sha256:d3fc4d8c7018207a9459a6b46a4fba594b0aed18502dbca5407ac1942631c963
size 16

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


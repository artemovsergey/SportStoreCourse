# Создание центра сертификации

- создайте файл ```openssl.conf```

```
[req]
distinguished_name = req_distinguished_name
prompt = no
default_bits = 2048
default_md = sha256
[req_distinguished_name]
CN = localhost
emailAddress = test@git.scc
```

- выполните команду только в ```Git Bash```

```
 openssl req -x509 -newkey rsa:4096 -days 365 -nodes -keyout root_ca.key -out root_ca.crt -config openssl.conf
```
На выходе генерируются два файла: root_ca.key и root_ca.crt


# Генерация приватного ключа

```
openssl genrsa -out localhost.key 2048
```
На выходе генерируются ```localhost.key```


# Создание запроc на подписывание ключа к центру сертификации

```
openssl req -new -key localhost.key -out localhost.csr -config openssl.conf
```

На выходе генерируются ```localhost.csr```

# Подпись запроса нашим центром сертификации 

- создайте файл ```localhost.ext```

```
authorityKeyIdentifier=keyid,issuer
basicConstraints=CA:FALSE
subjectAltName=@alt_names
[alt_names]
DNS.1=localhost
DNS.2=backend
IP.1=127.0.0.1
IP.2= {id}
```
**Замечание**: вместо {ip} подставьте ip вашего компьютера


- выполните команду

openssl x509 -req \
-CA root_ca.crt \
-CAkey root_ca.key \
-in localhost.csr \
-out localhost.crt \
-days 365 \
-CAcreateserial \
-extfile localhost.ext


На выходе генерируются ```localhost.crt```


# Установить центр сертификации в Chrome

- настройки -> безопасность -> сертификаты

# Настройка ssl для localhost в приложении Angular:

- в файле ```angular.json``` вставьте секцию ```options``` в секцию ```serve``` c указанием пути к ssl сертификату

angular.json
```json
   "options": {
            "sslCert": "./src/crt/localhost.crt",
            "sslKey": "./src/crt/localhost.key",
            "ssl": true
```

- запустите приложение ```npm run start``` или ```ng serve```. Приложение должно работать на https
**Примечание**: установка инструмента командной строки Angular

```npm install -g @angular/cli@latest```
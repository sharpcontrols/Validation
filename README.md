# Validation

## Validations for Numbers
- required - The field must not be null or empty
- positive - The field must be positive
- negative - The field must be negative
- max:{VALUE} - The field must be lower than VALUE
- min:{VALUE} - The field muss be higher than VALUE
- between:{VALUE1},{VALUE2} - The field must be between VALUE1 and VALUE2
- equal:{VALUE} - The field must be equal to VALUE (Only use an integer value for this, due to floating point errors)
- not_equal:{VALUE} - The field must not be equal to VALUE (Only use an integer value for this, due to floating point errors)


## Validations for Strings
- required - The field must not be null or empty
- accepted - The field must be "true", "1" or "yes"
- numeric - The field must be only numbers
- integer - The field must be only numbers and not decimal
- alpha - The field must be only Unicode alphabetic characters
- alpha_dash - The field must be only Unicode alphabetic characters and - or _
- email - The field must be a valid email address
- cpf - The field must be a valid CPF, all characters other than numbers will be stripped
- cnpj - The field must be a valid CNPJ, all characters other than numbers will be stripped
- pis - The field must be a valid PIS, all characters other than numbers will be stripped
- ip_address - The field must be a valid IP Address (IPv4 or IPv6)
- ipv4 - The field must be a valid IPv4 address
- ipv6 - The field must be a valid IPv6 address
- uppercase - The field must be all uppercase
- lowercase - The field must be all lowercase
- url - The field must be a valid url
- url_http - The field must be a valid http url 
- url_https - The field must be a valid https url
- url_ftp - The field must be a valid ftp url
- url_ftps - The field must be a valid ftps url
- url_ssh - The field must be a valid ssh url
- url_file - The field must be a valid file url
- url_mailto - The field must be a valid mailto url
- datetime - The field must be in a valid datetime format
- max:{VALUE} - The field has maximum VALUE letters
- min:{VALUE} - The field has minimum VALUE letters
- len:{VALUE} - The field has exactly VALUE letters
- between:{VALUE1},{VALUE2} - The field is between VALUE1 and VALUE2 letters long
- phone:{VALUE} - The field is a valid phone number for VALUE country (3 letter code)
- cellphone:{VALUE} - The field is a valid cellphone number for VALUE country (3 letter code)
- landline:{VALUE} - The field is a valid landline number for VALUE country (3 letter code)
- starts_with:{VALUE} - The field starts with VALUE
- ends_with:{VALUE} - The field ends with VALUE
- contains:{VALUE} - The field contains VALUE
- like:{VALUE} - The field is like VALUE (Similar to MySQLs LIKE)
- equal:{VALUE} - The field is the same as VALUE
- not_equal:{VALUE} - The field is not the same as VALUE 

docker build -t globo-clima/globo-clima . 
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 908027378551.dkr.ecr.us-east-1.amazonaws.com
docker tag globo-clima/globo-clima:latest 908027378551.dkr.ecr.us-east-1.amazonaws.com/globo-clima/globo-clima-api:latest
docker push 908027378551.dkr.ecr.us-east-1.amazonaws.com/globo-clima/globo-clima-api:latest
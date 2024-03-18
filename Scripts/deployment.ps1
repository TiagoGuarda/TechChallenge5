#Os comandos abaixo devem ser executados no powershell dentro do diretório raiz da solução

#Comando para fazer o build da imagem docker da API (Producer) e o push
$root = pwd
docker build -f $root\API.Producer\Dockerfile -t tiagoguarda/apiproducer .
docker push tiagoguarda/apiproducer

#Comando para fazer o build da imagem docker do Worker (Consumer) e o push
$root = pwd
docker build -f $root\Worker.Consumer\Dockerfile -t tiagoguarda/workerconsumer .
docker push tiagoguarda/workerconsumer

#Comando para criar um Service com as imagens da API, Worker e do RabbitMQ
kubectl apply -f YAML/pod-service.yml

#Comando para publicar um Pod com as imagens da API, Worker e do RabbitMQ
kubectl apply -f YAML/pod.yml

#Comando para verificar se o deployment do pod ocorreu corretamente
kubectl get pods

#Comando para verificar a configuração do service
kubectl get service

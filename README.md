# Introdução

SSE (Server Sent Events) é uma tecnologia que nos permite "ficar pendurado" em um servidor Http recebendo dados continuamente.

Diferente do Websocket, o SSE permite apenas o consumo de dados pelo cliente, não sendo possível enviar nenhuma informação para o servidor.
Em resumo, o Websocket tem comunicação bi-direcional (cliente <-> server) e o SSE unidirecional (Server -> Client).

Esse tipo de tecnologia é muito útil em cenários onde precisamos ter atualizações em tempo real de dados (Que é o caso que esse exemplo se propõe a mostrar). Pelo fato de ser uma via de mão única, acaba sendo mais performático do que o Websocket em muitos casos.

Mais informações em:

- https://html.spec.whatwg.org/multipage/server-sent-events.html#the-eventsource-interface
- https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events

# Sobre o Repositório

## MyCrypto-SSE
Exemplo de aplicação que utiliza SSE simulando um consumo em "real time" dos valores de crypto moedas

**Reforço aqui que esse projeto serve única e exclusivamente para estudos. Colocar um projeto SSE em produção não é tão trivial, principalmente no que diz respeito a escalabilidade. Produção é vida real, e todos nós sabemos que 'O mundo é diferente da ponte pra cá'...**

### MyCrypto.Backend

Web API que expõe stream de dados via SSE com informações fakes sobre preços de crypto moedas.

Note que, todas as informações de valores são criadas aleatoriamente.

Esse exemplo **simula** de maneira bem simplória um consumo de dados de um serviço de data stream como o Kafka.

Para esse exemplo, o consumo das informações é indiferente, mas no mundo real, isso impactaria a forma de escalar a sua aplicação. O foco aqui não é entrar nesse mérito. 

Para efeito de testes, o CORS nesse Web Api está sendo ignorado. Nunca faça isso sem entender profundamente como essa feature funciona. 

### MyCrypto.Playground

Console Application que consome os dados do Web API. 
Serve apenas para demonstrar como consumir dados de SSE usando o HttpClient.

### MyCrypto.Site
Exemplo de consumo do Web API usando JS.
É um site simples com apenas dois arquivos, index.html e sse.js. 
Não sou (e nem pretendo ser) especialista em JS. 

Utilizei a primeira lib que encontrei no Github para esse fim: https://github.com/mpetazzoni/sse.js.
Funcionou de primeira. Achei bem simples de usar

Para rodar o site eu usei o dotnet-serve, que é uma ferramente bem legal para subir um servidor HTTP por linha de comando de forma simples e rápida. É o tipo de ferramente que facilita muito o dia-a-dia.

Nesse link https://github.com/natemcmaster/dotnet-serve é possível ter todas as informações para instalação e uso da ferramenta.

Considere apoiar o desenvolvedor dessa ferramenta ❤️

Após instalar o dotnet-serve já é possível rodar o projeto. Para isso exeute os seguintes comandos: 

**Para inicar o Backend:**
```
cd MyCrypto-SSE/src/MyCrypto.Backend 
dotnet run
```

**Para inicar o Playground:**
```
cd MyCrypto-SSE/src/MyCrypto.Playground 
dotnet run
```

**Para inicar o Site:**
```
cd MyCrypto-SSE/src/MyCrypto.Site 
dotnet serve -o -S -p 51234
# -o faz com que o navegador seja aberto
# -S roda o site usando https
# -p indica a porta onde a aplicação vai rodar
```
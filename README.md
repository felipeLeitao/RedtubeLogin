# RedtubeLogin
Quem nunca desenvolveu um login em algum site que utilizasse a api do facebook? E que tal você desenvolver um login pro seu site usando o redtube? Simples assim! Você fornece o usuario e senha, e api vai te retornar o código e se o usuário está com o email confirmado.

#Como uso?
Simplesmente faça uma requisição HTTP utilizando o método GET no seguinte formato:

http://redtube.azurewebsites.net/api/login?usuario=TesteLogin&senha=Teste123

#Exemplo de retorno

O retorno será sempre no formato JSON.

{
Codigo: 4528108,
Usuario: "TesteLogin",
UrlAvatar: "http://img.l3.cdn.redtubefiles.com/_thumbs/design/profile_male.png",
isAtivo: false
}

#Próximos Passos

1 - Realizar o pedido utilizando uma requisição HTTP POST, pra ter mais segurança.
2 - Montar um Json Schema para o request e um para o response




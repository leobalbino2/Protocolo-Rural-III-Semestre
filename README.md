<p align="left" style="font-size:28px;"><strong><em>DOCUMENTO DA APLICAÇÃO WEB</em></strong></p>

![Preview][lg]

<details>
  <summary><strong>📑 Sumário</strong></summary>

- [1. Introdução](#1-introdução)
  - [Objetivos](#-objetivos)
  - [Metodologia](#-metodologia)
- [2. Requisitos](#2-requisitos)
  - [Requisitos funcionais](#-requisitos-funcionais)
  - [Requisitos não funcionais](#-requisitos-não-funcionais)
- [3. Modelo de casos de uso](#3-modelo-de-casos-de-uso)
- [4. Modelo do banco de dados](#4-modelo-do-banco-de-dados)
- [5. Banco de dados](#5-banco-de-dados)
- [6. Diagrama de classes](#6-diagrama-de-classes)
- [7. Estudo de viabilidade](#7-estudo-de-viabilidade)
- [8. Regras de negócio (Modelo canvas)](#8-regras-de-negócio-modelo-canvas)
- [9. Design](#9-design)
- [10. Protótipo](#10-protótipo)
- [11. Aplicação](#11-aplicação)
- [12. Considerações Finais](#12-considerção-finais)

</details>





# 1. Introdução
O conceito de sustentabilidade surgiu da necessidade de refletir sobre como a sociedade usa e explora os recursos naturais. O intuito é buscar alternativas que preservem esses recursos, evitando o esgotamento e garantindo sua disponibilidade para o futuro.  

A crescente preocupação com a sustentabilidade nas práticas agrícolas reforça a necessidade de ferramentas acessíveis, de fácil utilização e compreensão, que ajudem os produtores rurais, técnicos e gestores municipais a monitorar e melhorar a sustentabilidade de suas atividades.  

A aplicação web de Protocolo Rural foi desenvolvida para substituir a planilha de Protocolo de Avaliação da Sustentabilidade Rural para o Ecótono do Centro Oeste Paulista criada no excel pelo aluno Lucas Vinicius de Pieri como parte de seu TCC.  O projeto busca classificar o grau de sustentabilidade das propriedades rurais, aos interessados, por meio de questionários construídos com base em indicadores ambientais, sociais e econômicos, para que os usuários possam refletir e possivelmente aprimorar os procedimentos usados em suas propriedades rurais.  

## • Objetivos

### Geral 
Desenvolver uma Plataforma Web que possibilite classificar o grau de sustentabilidade de propriedades rurais na região agrícola de Jaú/SP, em substituição ao instrumento utilizado atualmente (planilha do excel).  

### Específicos 
• Estudar a dinâmica da Planilha de Protocolo de Avaliação da Sustentabilidade Rural para o Ecótono do Centro Oeste Paulista.
Identificar os indicadores de Sustentabilidade (parâmetros utilizados para       localizar áreas que afetam a sustentabilidade de um agrossistema) e formular como podem ser transferidos e implementados em uma aplicação web. 

 
• Pesquisar aplicações web com objetivos similares  
Investigar características do design e funcionalidades de aplicações web semelhantes ao tema do projeto. 

### Metodologia
Ao desenvolver a aplicação web de Protocolo Rural, serão utilizadas diversas ferramentas e tecnologias apresentadas no curso de Desenvolvimento de Software Multiplataforma (DSM), integrando conceitos abordados ao longo das aulas. Algumas dessas ferramentas incluem tecnologias de desenvolvimento front-end e back-end, além de metodologias de engenharia de software. 

 

### Linguagens de Programação 
Determinadas linguagens de programação serão necessárias na criação do projeto, especificamente HTML5, CSS3 e JavaScript, com o intuito de se construir uma interface estática que possui estrutura interativa e responsiva. 

 

### Frameworks 
A utilização do framework CSS chamado bootstrap permite agilizar o desenvolvimento front-end e garantir consistência visual em toda a aplicação. 

### Prototipagem  
A plataforma de design e criação de interfaces Figma possibilitará a concepção de protótipos das telas do projeto permitindo visualizar modelos experimentais da aplicação web. 

### Metodologia Scrum 
Este projeto integrador utilizará o modelo Scrum, devido à sua estrutura ágil e iterativa, que permite o trabalho com ciclos curtos de desenvolvimento (Sprints) que habilitando inspeção e adaptação contínuas, otimizando recursos e garantindo alinhamento frequente com as necessidades do projeto.   

# 2. Requisitos
O documento de requisitos de uma aplicação é um fator fundamental para seu desenvolvimento, pois permite catalogar e compreender quais características são essenciais para o sistema a ser desenvolvido com base nas funcionalidades e restrições propostas pelo cliente, assim possibilitando a entrega de um produto padronizado e satisfatório conforme as especificações solicitadas. 

### Histórias do usuário 
• Como usuário não cadastrado, quero poder acessar a sessão "Sobre" do site para que eu possa entender como funciona. 

• Como usuário não cadastrado, eu quero poder acessar a sessão "Contato" do site para me comunicar com os desenvolvedores. 

• Como usuário não cadastrado, eu quero poder acessar a página "Quem somos” para saber mais sobre os desenvolvedores do projeto. 

• Como usuário não cadastrado, eu quero poder acessar a sessão “Cadastro” do site para me cadastrar na plataforma. 

• Como usuário cadastrado eu quero poder acessar a sessão "Configurações" para mudar minhas informações de conta. 

• Como usuário cadastrado, eu quero poder realizar uma avaliação para entender o grau de sustentabilidade da minha propriedade rural. 

• Como usuário cadastrado eu quero poder receber um aviso para me alertar caso eu não preencha todos os indicadores do formulário. 

• Como usuário cadastrado, eu quero poder ver os resultados da minha avalição em gráficos para que eu possa entender melhor a sustentabilidade da minha propriedade rural. 

• Como usuário cadastrado, eu quero poder acessar o meu histórico de avalições para possíveis comparações. 

• Como administrador eu quero poder acessar a sessão "Cadastrar indicador" para criar um novo indicador. 

• Como administrador eu quero poder excluir um indicador, para remover os indicadores que não forem mais relevantes. 

• Como administrador eu quero poder editar um indicador, para alterá-lo caso necessário. 

• Como administrador eu quero poder desativar ou ativar um indicador para deixar avaliações condizentes com as condições atuais. 

## • Requisitos funcionais
Os requisitos funcionais de uma aplicação são especificações definidas na etapa de elicitação do software, que ocorre durante o levantamento de requisitos. Seu propósito é fornecer à equipe de desenvolvimento as características especificas, restrições e funcionalidades que o sistema possui durante certas circunstâncias, estabelecendo assim as bases para o desenvolvimento de um produto que atende as necessidades do cliente. 



### Usuários Não Cadastrados 


#### RF 1 – Mostrar página sobre o site 
Apresentar uma página com informações sobre a aplicação web, como sua descrição, objetivos e sobre os desenvolvedores. 

 
#### RF 2 – Mostrar página quem somos 
Desenvolver uma página sobre quem são os desenvolvedores do projeto, incluindo links para seus respectivos LinkedIn e GitHub. 

 
#### RF 3 – Exibir página contato 
Implementar a página contato onde o usuário poderá enviar uma mensagem preenchendo seu e-mail, assunto e mensagem para se comunicar com os desenvolvedores do projeto. 

 
#### RF 4 – Cadastrar usuário 
Possibilitar que o usuário crie uma conta ao fornecer dados básicos com o intuito de salvar suas avaliações. 

 

### Usuários Cadastrados 


#### RF 5 – Acessar o sistema 
Permitir que o usuário entre no sistema fornecendo seus dados previamente cadastrados, e-mail e senha para gerenciar dados de seu perfil, visualizar seu histórico de avaliações e realizar login e logout do sistema. 


#### RF 6 – Editar perfil 
Permitir que o usuário cadastrado possa editar todos os seus dados, como e-mail, nome de usuário, telefone e senha. 

 
#### RF 7 – Realizar avaliação 
Proporcionar ao usuário um formulário com todos os indicadores de sustentabilidade, onde poderá realizar o preenchimento, respondendo cada um dos indicadores ao selecionar a categoria em que a situação de sua propriedade rural se enquadra. 

 
#### RF 8 – Validar o preenchimento do questionário 
Verificar se todos os indicadores da avaliação foram completamente preenchidos antes de mostrar o resultado ao usuário, caso contrário, o sistema mostrará um aviso. 

 
#### RF 9 – Apresentar resultados 
Exibir os resultados da avaliação ao ser concluída, calculando as pontuações de cada um dos indicadores de sustentabilidade e mostrando uma porcentagem que representa o grau de sustentabilidade daquela propriedade. 

 
#### RF 10 – Gerar gráfico 
Mostrar gráficos que representem o grau de sustentabilidade e outro que representa o resultado da avaliação por indicador. 

 
#### RF 11 – Arquivar resultados 
Proporcionar ao usuário cadastrado o armazenamento de avaliações anteriores com seus respectivos resultados para comparações futuras. 

 
#### RF 12 – Exibir histórico de avaliações 
Disponibilizar ao usuário o acesso ao histórico completo de suas avaliações passadas onde ele poderá visualizar a data, as notas referentes a cada indicador e o grau de sustentabilidade, possibilitando a comparação dos resultados anteriores. 

 

### Administradores 


#### RF 12 – Cadastrar indicador 
Conceder ao administrador a capacidade de criar indicadores de sustentabilidade, informando nome e descrição, para que possam ser utilizados nas avaliações. 

 

#### RF 13 – Excluir indicador 
Permitir que o administrador exclua indicadores que não são mais relevantes ou necessários para as avaliações. 

 

#### RF 14 – Editar indicador 
Oferecer ao administrador a opção de modificar indicadores existentes para ajustá-los conforme necessário. 

 

#### RF 15 – Ativar/desativar indicador 
Habilitar o administrador a ativar ou desativar indicadores, permitindo ajustes nas avaliações de acordo com as condições atuais ou mudanças nas diretrizes. 



## • Requisitos não funcionais
Os requisitos não funcionais referem-se aos aspectos do sistema que garantem sua qualidade. 

 
 #### RNF 1 – Usabilidade 
 A aplicação deve ser fácil de entender e usar, com uma interface limpa e formulários diretos, com explicações claras dos indicadores, para que diferentes tipos de usuários possam navegar sem problemas. 

 
 #### RNF 2 – Desempenho 
 O sistema deve responder de forma rápida, garantindo que os usuários possam ver os resultados sem demora, especialmente em áreas rurais com conexão mais fraca. 

 
 #### RFN 3 – Acessibilidade 
 O sistema deve ser acessível para todos os tipos de usuários, incluindo aqueles com necessidades especiais. 

 
#### RFN 4 – Compatibilidade 
O sistema deve funcionar uniformemente em diferentes navegadores e dispositivos, móveis ou desktop. 

# 3. Modelo de casos de uso
O diagrama de casos de uso, serve de representação visual que mostra as funcionalidades principais de um sistema. Ele descreve as interações entre os atores (usuários) e o sistema por meio de "casos de uso", que são ações ou serviços oferecidos. 

![Preview][cdu]

### CASOS DE USO RESUMIDOS

Caso de Uso: Entrar em contato
Atores envolvidos: Usuário.
Visão geral: O usuário acessa a página de contato da plataforma e preenche os campos: nome, assunto, mensagem e e-mail. Após fornecer essas informações o usuário clica no botão "enviar" e a mensagem é encaminhada para o administrador.

Caso de Uso: Cadastrar no sistema
Atores envolvidos: Usuário.
Visão geral: O usuário entra no sistema e clica no botão "Acessar", localizado no canto superior direito da página inicial, são exibidas as opções para login e cadastro. O usuário se ainda não cadastrado, clica na opção "Cadastrar-se" e é redirecionado para uma página onde deve fornecer seus dados pessoais, após preencher seus dados, o usuário se cadastra e sua conta é criada no sistema.

Caso de Uso: Realizar login
Atores envolvidos: Usuário, Administrador.
Visão geral: O usuário acessa a plataforma e clica no botão "Acessar" localizado no canto superior direito da tela, em seguida, preenche os campos "e-mail" e "senha" com os dados cadastrados, e clica no botão "Entrar", sendo logado no sistema.

Caso de Uso: Gerenciar indicadores
Atores envolvidos: Administrador.
Visão geral: O administrador acessa o sistema e tem a permissão para adicionar ou remover indicadores se necessário.

### CASOS DE USO DETALHADO
Caso de uso detalhado: Realizar avaliação
Ator 1: Usuário (Proprietário Rural).

#### Pré-Condições
O usuário deve ter uma conta cadastrada e estar logado no sistema.

#### Fluxo Principal (Sucesso):
1.	Preenchimento do nome da avalição
O usuário preenche o campo de nome para dar um nome à sua avaliação.
2.	Preenchimento do formulário de indicadores
O usuário responde as questões do formulário, selecionando as opções que mais condizem com a realidade de sua propriedade.
3.	Validação do formulário
O sistema verifica se todas as questões foram devidamente respondidas.
4.	Finalização da avaliação
O usuário clica no botão "Finalizar", para finalizar sua avaliação.
5.	Apresentação dos resultados
Os resultados são armazenados e exibidos no painel de avaliações, em formato de notas, porcentagem e gráficos.

#### Fluxo Alternativo:
1.	Formulário não é preenchido totalmente 
O usuário não consegue dar continuidade na sua avaliação enquanto não responder à questão que falta.

#### Pós Condições: 
A avaliação é finalizada e seus resultados são armazenados e ficam disponíveis para visualização no painel de avaliações.



# 4. Modelo do banco de dados
(Modelo conceitual, Modelo lógico, Físico)

# 5. Banco de dados
O banco de dados foi desenvolvido de acordo com as necessidades da aplicação, garantindo que as informações fossem armazenadas de forma flexível e adequada ao tipo de dados trabalhado.

Foi utilizado o MongoDB como sistema de banco de dados NoSQL por oferecer alta flexibilidade na modelagem e boa performance para grandes volumes de dados, o que se integra facilmente com aplicações modernas.

A criação e gestão do banco foram realizadas utilizando o MongoDB Compass. Essa ferramenta facilita a visualização dos documentos, a criação de coleções, a execução de consultas utilizando a sintaxe do MongoDB e o acompanhamento do funcionamento geral do banco. Elas foram essenciais para controlar a estrutura dos dados.

# 6. Diagrama de classes
O diagrama de classe é uma representação visual da estrutura de um sistema orientado a objetos.

![Preview][ddc]


# 7. Estudo de viabilidade
O estudo de viabilidade é uma avaliação preliminar que determina se um projeto é possível de ser executado através da examinação de aspectos técnicos, econômicos, legais e operacionais para identificar potenciais obstáculos antes do início do projeto e se ele é viável ou não.

### VIABILIDADE TÉCNICA
A viabilidade técnica avalia se um projeto pode ser implementado com as tecnologias e recursos disponíveis, examinando se a equipe possui as habilidades técnicas necessárias e se a infraestrutura existente suporta os requisitos do sistema proposto. No caso do Protocolo Rural, o projeto está sendo desenvolvido por alunos capacitados do curso de Desenvolvimento de Software Multiplataforma, utilizando tecnologias como HTML, CSS, JavaScript e PHP. Além disso, são empregadas plataformas como o GitHub, para controle de versão, e o Figma, para a prototipação das interfaces.

###	VIABILIDADE FINANCEIRA
Através do estudo da viabilidade financeira, é possível examinar os aspectos econômicos do projeto, analisando custos de desenvolvimento, implementação e manutenção em relação aos potenciais retornos financeiros. Este estudo determina se o projeto é economicamente sustentável e se há recursos suficientes para sua execução e continuidade.
O projeto é feito por estudantes com o intuito de disponibilizar uma aplicação completamente gratuita, sem qualquer fim lucrativo ou monetização, assim não sendo necessário um estudo aprofundado sobre a viabilidade financeira do projeto, pois a longo prazo, não haverá retorno ou lucros.

###	VIABILIDADE MERCADO
A viabilidade de mercado avalia se existe demanda suficiente para o produto ou serviço, analisando o público-alvo, a concorrência e o potencial de aceitação no mercado. Envolve a identificação de necessidades não atendidas e oportunidades disponíveis no segmento de mercado escolhido. 
A aplicação atende a uma demanda crescente no setor agropecuário: a necessidade de uma ferramenta que facilite o monitoramento e a melhoria da sustentabilidade em uma propriedade rural. O Protocolo Rural propõe uma alternativa intuitiva, gratuita e de fácil utilização, tornando o processo mais eficiente para os proprietários rurais da região do centro-oeste paulista.

###	VIABILIDADE OPERACIONAL
A viabilidade operacional analisa se um projeto pode ser executado de forma prática e eficiente, levando em conta os recursos humanos, o ambiente de uso e a aceitação do sistema pelos usuários finais. 
Do ponto de vista operacional, o projeto é plenamente viável. A plataforma foi pensada para ser funcional tanto em computadores quanto em dispositivos móveis, considerando o público-alvo que pode acessar o sistema em regiões com conexão limitada. A interface foi desenhada com foco em simplicidade, clareza e acessibilidade, o que facilita seu uso mesmo por usuários com pouca familiaridade com tecnologia.

###	CONCLUSÃO
O estudo de viabilidade do Protocolo Rural mostra que o projeto é realizável sob aspectos financeiros, operacionais e técnicos. 
Mesmo sendo um projeto sem fins lucrativos, os custos são reduzidos, e sua execução se mostra sustentável no contexto acadêmico. Além disso, há uma demanda concreta no setor agropecuário por soluções que facilitem o monitoramento da sustentabilidade rural.


# 8. Regras de negócio (Modelo canvas)
![Preview][mdn]

###	O QUE SERÁ REALIZADO?
 __Proposta de Valor__

Uma aplicação web que será uma ferramenta prática e estruturada, de fácil usabilidade e acesso, capaz de avaliar as condições de sustentabilidade, baseando-se em diversas áreas (indicadores) que contribuem para o bom funcionamento do agroecossistema de uma propriedade rural.

### COMO SERÁ REALIZADO?
__Parcerias-Chave__

Faculdade de Tecnologia de Jahu (FATEC Jahu).

__Atividades-Chave__

Desenvolver uma plataforma que proporciona um questionário de fácil acesso que visa identificar as condições de propriedades rurais. O sistema permite a elaboração estruturada da avaliação através de indicadores específicos que fornecem ao usuário uma porcentagem de sustentabilidade. Além disso a plataforma contará com manutenção, garantindo um bom funcionamento do sistema.

__Recursos-Chave__

Tempo e dedicação da equipe, junto com a documentação do sistema e       ferramentas para desenvolver o projeto.

### PARA QUEM SERÁ REALIZADO?
__Relacionamento com Clientes__

Contato por Email e feedback dos clientes.

__Canais de Distribuição__

O projeto estará disponível na nossa aplicação, nas redes moveis e redes sociais, e através de instituições de ensino.

__Segmento de Clientes__

Proprietários rurais, estudantes, pesquisadores, empresas agrícolas, órgãos ambientais entre outros membros da sociedade.

###	QUANTO CUSTARÁ?
__Estrutura de Custos__

Desenvolvimento e manutenção da aplicação web, hospedagem, tempo e suporte. 

__Fontes de Receita__

Projeto sem fins lucrativos, focado em desenvolver uma aplicação web para ajudar a avaliar a sustentabilidade de propriedades rurais, assim contribuindo para boas práticas ambientais.


# 9. Design

### Paleta de Cor
As cores escolhidas para o projeto foram selecionadas através de um esquema de cores análogas, uma combinação de três cores próximas uma da outra em um círculo cromático. 
A paleta do projeto tem o intuito de representar a transição ambiental entre os biomas da mata atlântica e cerrado, que se caracterizam por seus tons de verdes e amarelos.

![Preview][pdc]

### Tipografia
Para o projeto, foi escolhida a fonte Open Sans, uma tipografia sem serifa que se destaca pela sua incrível versatilidade. Além de oferecer ótima legibilidade em diferentes aparelhos, desde desktops até dispositivos moveis, ela mantém sua clareza e definição tanto em meios digitais quanto impressos.

![Preview][fnt]

### Logo
O tipo de logo escolhido foi o Isotipo, misturando a tipografia Open Sans, uma fonte moderna e simples. Já o símbolo foi elaborado para representar o equilíbrio entre sustentabilidade e o agronegócio.

![Preview][lg]

### Wireframe
O wireframe serve como uma base para como uma estrutura inicial da aplicação Protocolo Rural poderia se parecer. 
Atualmente ela se localiza no link à baixo:

[Wireframe Protocolo Rural](https://www.figma.com/design/pcAL45RIRkzJonJIfegYL8/Wireframe---Protocolo-Rural?node-id=0-1&t=fn86gsElgTURtnbu-1 "hover text")


# 10. Protótipo
O protótipo do projeto foi criado através da plataforma de design e prototipagem Figma. O link segue logo à baixo.

[Protótipo Protocolo Rural](https://www.figma.com/design/oWrjSVdJYFZ6AjlZ1ABUgw/Prot%C3%B3tipo---Protocolo-Rural?node-id=0-1&t=aeE2ZFcHH2HeqOoj-1 "hover text")


# 11. Aplicação
A página para o GitHub do projeto se encontra no link abaixo:

[Aplicação Protocolo Rural](https://github.com/leobalbino2/Protocolo-Rural-III-Semestre "hover text")


# 12. Considerações Finais
O terceiro semestre do projeto trouxe desafios que influenciaram diretamente esta etapa. Um dos principais pontos foi a necessidade de ampliar nosso conhecimento técnico em todas as partes do sistema, especialmente no desenvolvimento em C#. Além disso, enfrentamos a falta de tempo necessário para concluir todas as funcionalidades planejadas.

Mesmo com essas dificuldades, conseguimos entregar uma versão funcional que atende aos objetivos centrais definidos para o semestre. Os resultados foram satisfatórios, embora a equipe reconheça que ainda há espaço para melhorias e ajustes. Continuamos motivados a avançar no projeto, buscando torná-lo mais completo e fiel à proposta original.


[cdu]: imgs/casodeuso.jfif
[ddc]: imgs/diagramaclasse.jpeg
[lg]: imgs/logo.jpeg
[mdn]: imgs/modelonegocios.jpeg
[pdc]: imgs/paleta.jpeg
[fnt]: imgs/fonte.jpeg



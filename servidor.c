#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <string.h>
#include <pthread.h>

//Version del servidor a 1/05/2023
//************************************************************************************************
//Estructuras
typedef struct{
	char nombre[20];
	int socket;
}Conectado;

typedef struct{
	Conectado conectados[100];
	int num;
}ListaConectados;
//************************************************************************************************
//Variables globales
ListaConectados milistaconectados;
char conectados[300];
int sockets[100];
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER; //Estructura para acceso excluyente

//************************************************************************************************
//Funciones
int AnadirConectado(char nom[20], int socket, ListaConectados *l){
	if(l->num == 100){
		return -1;
	}
	else{
		printf("Entro\n");
		pthread_mutex_lock(&mutex);
		strcpy(l->conectados[l->num].nombre, nom);
		l->conectados[l->num].socket = socket;
		l->num+=1;
		pthread_mutex_unlock(&mutex);
		printf("Salgo\n");
		return 0;
	}
}

int DameSocket(ListaConectados *l, char nom[20]){
	int i =0;
	int encontrado =0;
	while((encontrado) && (l->num != i)){
		if(strcmp(l->conectados[i].nombre,nom)==0)
			encontrado = 1;
		else
			i++;
	}
	if(encontrado){
		return -1;
	}
	else if(!encontrado){
		int n = l->conectados[i].socket;
		return n;
	}
	
}

int DamePosicion(int *socket, ListaConectados *l){
	int i =0;
	int encontrado =0;
	while((!encontrado) && (l-> num != i)){
		if(l->conectados[i].socket==socket)
			encontrado = 1;
		else
			i++;
	}
	if(!encontrado)
		return -1;
	if(encontrado)
		return i;
}
int EliminarConectado(int *socket, ListaConectados *l){
	int p = DamePosicion(socket, l);
	if(p == -1)
		return -1;
	else{
		for(int i = p; i< l->num; i++){
			pthread_mutex_lock(&mutex);
			l->conectados[i] = l->conectados[i+1];
			l->num = (l->num)-1;
			pthread_mutex_unlock(&mutex);
		}
		return 0;
	}
}
void DameConectados(char VectorConectados[500], ListaConectados *l){
	//sprintf(respuesta,"%d/",l->num);
	for(int i =0; i<l->num; i++){
		sprintf(VectorConectados,"%s%s/",VectorConectados,l->conectados[i].nombre);
	}
	printf("%s\n", VectorConectados);
}

void EnviarListaConectados(char VectorConectados[500],ListaConectados *l){
	int i=0;
	while(i<l->num){
		//DameSocket(&l,l->conecados[i].nombre);
		printf("Envio a %s, con socket, %d y le mando %s\n",l->conectados[i].nombre,l->conectados[i].socket,VectorConectados);
		write(l->conectados[i].socket,VectorConectados,strlen(VectorConectados));
		i++;
	}
}
//**************************************************************************************************
//Atender Clientes
void *AtenderCliente(void * socket)
{
	
	int sock_conn = *((int *) socket);
	
	char peticion[500];
	char respuesta[500];
	//char respuesta2[500];
	char LConectados[500];
	
	int ret;
	
	char logged[20];
	int terminar =0;
	
	MYSQL *conn;
	int err;
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta [80];
	conn = mysql_init(NULL);
	char str_query[512];
	char str_query1[512];
	int partidas_ganadas;
	
	conn=mysql_init(NULL);
	if (conn==NULL)
	{
		printf ("Error al crear la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "juego",0, NULL, 0);
	if (conn==NULL)
	{
		printf ("Error al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	while(!terminar){
			printf("--------------------------------------------------------------------\n");
			peticion[0] = '\0';
			// recibimos la peticion
			ret = read(sock_conn, peticion,sizeof(peticion));
			printf("Recibido\n");
			
			//Tenemos que anadirle la marca de fin de string
			//para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			printf ("Peticion: %s\n",peticion);
			
			//Recibir la peticion
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el codigo de la peticion
			char nombre[20];
			printf ("Codigo: %d\n", codigo);
			
			
			if (codigo ==0) {
				terminar=1;
				EliminarConectado(sock_conn, &milistaconectados);
			}//peticion de desconexion
				
			else
			{
				if(codigo==1){ //logearse
					p = strtok( NULL, "/");
					strcpy (nombre, p);
					p=strtok(NULL,"/");
					char contrasenya[20];
					strcpy(contrasenya,p);
					printf ("Nombre: %s\n", nombre);
					printf("%s %s \n",nombre, contrasenya);
					sprintf(str_query, "SELECT jugadores.idj,jugadores.usuario FROM jugadores WHERE usuario= '%s' AND contrasenya='%s';", nombre,contrasenya);
					err=mysql_query (conn, str_query);
					printf("1\n");
		
					if (err!=0) {
						printf ("Error al consultar datos de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						exit (1);
					}
					
					resultado = mysql_store_result (conn);
					row =  mysql_fetch_row (resultado);
					if (row == NULL)
						strcpy (respuesta,"1/1/\n");
					else{
						//row = mysql_fetch_row(resultado);
						//sprintf(respuesta,"Bienvenido %s\n",row[1]);
						strcpy(respuesta,"1/2\n");
						AnadirConectado(nombre, sock_conn,&milistaconectados);
					}
					printf("Correcto %s\n",respuesta);
					write (sock_conn,respuesta, strlen(respuesta));
				}
				else if (codigo ==2) //registrarse
				{
					p = strtok( NULL, "/");
					strcpy (nombre, p);
					p=strtok(NULL,"/");
					char contrasenya[20];
					strcpy(contrasenya,p);
					printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);
					printf("Nombre: %s, contrasenya: %s \n ",nombre, contrasenya);
					
					sprintf(str_query,"SELECT jugadores.usuario FROM jugadores WHERE jugadores.usuario=%s\n",nombre);
					err=mysql_query (conn, str_query);
					if (err!=0)
					{
						printf ("Error al consultar datos de la base para la id: %u %s \n",
								mysql_errno(conn), mysql_error(conn));
					}
					
					resultado = mysql_store_result (conn);
					row =mysql_fetch_row(resultado);
					
					if(row !=NULL){
						printf("El usuario se puede conectar ");
						strcpy(respuesta,"2/1/\n");
						write (sock_conn,respuesta, strlen(respuesta));
					}
						
					else{
						strcpy(respuesta,"2/2/\n");
						int id=0;
						strcpy(str_query,"SELECT MAX(idj) FROM jugadores;");
						err=mysql_query (conn, str_query);
						if (err!=0)
						{
							printf ("Error al consultar datos de la base para la id: %u %s \n",
									mysql_errno(conn), mysql_error(conn));
						}
						
						resultado = mysql_store_result (conn);
						row = mysql_fetch_row (resultado);
						if(row!=NULL){
							id = atoi(row[0]);
							id = id+1;
						}
						
						sprintf(str_query, "INSERT INTO jugadores VALUES ('%d','%s', '%s',%d);",id, nombre,contrasenya,0);
						err=mysql_query (conn, str_query);
						if (err!=0)
						{
							printf ("Error al consultar datos de la base %u %s \n",
									mysql_errno(conn), mysql_error(conn));
						
						}
						write (sock_conn,respuesta, strlen(respuesta));
					}
					
					
				}
				else if (codigo ==3){ // consulta 1: dame el nombre del jugador con la maxima puntuacion y su puntuacion
					strcpy(str_query,"SELECT jugadores.usuario, jugadores.puntuacion FROM jugadores WHERE puntuacion = (SELECT MAX(puntuacion) FROM jugadores);");
					printf("%s\n",str_query);
					err=mysql_query (conn, str_query);
					printf("prueba\n");
					
					if (err!=0)
					{
						printf ("Error al consultar datos de la base %u %s \n", mysql_errno(conn), mysql_error(conn));
					}
					
					
					else {
						
						resultado = mysql_store_result (conn);
						//strcpy(respuesta,"Los jugadores con mayor puntuacion son:\n");
						if (row == NULL){
							memset(respuesta,'\0',512);
							strcpy (respuesta,"3/1/\n");
						}
						
						else{
							while((row = mysql_fetch_row (resultado))!= NULL){
								sprintf (respuesta,"3/2/%s/%s\n", row [0],row[1]);
								//strcat(respuesta,respuesta2);
							}
						}
						
						write (sock_conn,respuesta, strlen(respuesta));
					}
				}
				
				else if (codigo ==4){ // consulta 2: dame la duracion de la partida mas larga que jugo x'
					p = strtok( NULL, "/");
					strcpy (nombre, p);
					printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);
					printf("%s\n",nombre);
					printf("a\n");
					printf("Nombre de la persona cuya partida mas larga quieres saber:");
					char nom[20];
					//scanf("%s", nom);
					sprintf(str_query,"SELECT partidas.duracion FROM partidas, participacion WHERE partidas.id= '%s' ", nom);
					printf("%s\n",str_query);
					err=mysql_query (conn, str_query);
					printf("prueba\n");
					if (err!=0)
					{
						printf ("Error al consultar datos de la base %u %s \n", mysql_errno(conn), mysql_error(conn));
					}
					else {
						resultado = mysql_store_result (conn);
						row = mysql_fetch_row (resultado);
						if (row == NULL)
							printf ("No se han obtenido datos en la consulta\n");
						else
							printf ("La duracion de la partida mas larga que jugo '%s' es: '%s'", nom, row [0] );
						
						printf("\n");
					}
				}
				
				else if (codigo ==5){ // consulta 3: dime si existe un jugador con el nombre de usuario x
					p = strtok( NULL, "/");
					strcpy (nombre, p);
					printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);
					printf("%s\n",nombre);
					printf("a\n");
					printf("Nombre:");
					char nom[20];
					scanf("%s", nom);
					sprintf(str_query,"SELECT jugadores.usuario FROM jugadores WHERE jugadores.usuario = '%s' ", nom);
					printf("%s\n",str_query);
					err=mysql_query (conn, str_query);
					printf("prueba\n");
					if (err!=0)
					{
						printf ("Error al consultar datos de la base %u %s \n", mysql_errno(conn), mysql_error(conn));
					}
					else {
						resultado = mysql_store_result (conn);
						row = mysql_fetch_row (resultado);
						printf("\n");
						
						if (row == NULL)
							printf ("No se han obtenido datos en la consulta\n");
						else{
							if(strcmp(row[0],nom)==0)
								printf ("Ya existe un usuario con el nombre: '%s'", nom);
							else
								printf ("No existe un usuario con el nombre: '%s'", nom);
							printf("\n");
						}
					}
				}
				
				memset(respuesta,0,sizeof(respuesta));
				if(codigo!=0){
					memset(LConectados,0,sizeof(LConectados));
					strcpy(LConectados,"6/");
					DameConectados(LConectados, &milistaconectados);
					EnviarListaConectados(LConectados,&milistaconectados);
					
					//strcpy(respuesta,LConectados);
					//write(sock_conn, LConectados,strlen(LConectados));
				}
			}
/*			printf("Conectados in\n");*/
/*			DameConectados(LConectados, &milistaconectados);*/
/*			EnviarListaConectados(LConectados,&milistaconectados);*/
			//memset(respuesta,0,sizeof(respuesta));
/*			printf("Conectados out\n");*/
			printf("si\n");
			printf("--------------------------------------------------------------------\n");
	}
	
	close(sock_conn);
}	// AtenderCliente




int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
		
	// asocia el socket a cualquiera de las IP de la m?quina.
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9095);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	if (listen(sock_listen, 4) < 0)
		printf("Error en el Listen");
		
	pthread_t thread;
	int i = 0;
	// Bucle infinito
	for (;;){
			printf ("Escuchando\n");
			
			sock_conn = accept(sock_listen, NULL, NULL);
			printf ("He recibido conexion\n");
			//sock_conn es el socket que usaremos para este cliente
			
			sockets[i] = sock_conn;
			
			pthread_create(&thread, NULL, AtenderCliente, &sockets[i]);
			i = i +1;
			printf("%d\n",i);
	}
}  //Main
	


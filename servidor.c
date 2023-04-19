#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>




int main(int argc, char *argv[])
{
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
	serv_adr.sin_port = htons(9060);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 2) < 0)
		printf("Error en el Listen");
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		int terminar =0;
		// Entramos en un bucle para atender todas las peticiones de este cliente
		//hasta que se desconecte
		while (terminar ==0)
		{
			printf("--------------------------------------------------------------------");
			// Ahora recibimos la petici?n
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			//Escribimos el nombre en consola
			printf ("Se ha conectado: %s\n",peticion);
			
			// vamos a ver que quieren
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el codigo de la peticion
			char nombre[20];
			printf("Codigo: %d \n", codigo);
			
			if (codigo ==0) //peticion de desconexion
			{
				terminar = 1;
			}
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
					sprintf(str_query, "SELECT idj FROM jugadores WHERE usuario= '%s' AND contrasenya='%s';", nombre,contrasenya);
					err=mysql_query (conn, str_query);
					printf("1\n");
					if (err!=0) {
						printf ("Error al consultar datos de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						exit (1);
					}
					else
					sprintf(respuesta, "Bienvenido %s", nombre);
					strcpy(str_query,"select * from Jugadores");
					printf("%s\n",str_query);
					err=mysql_query (conn, str_query);
					printf("prueba\n");
					write (sock_conn,respuesta, strlen(respuesta));
					mysql_close (conn);
				}
				else if (codigo ==2) //registrarse
				{
					int id,pu;
					p = strtok( NULL, "/");
					id = atoi(p);
					p = strtok( NULL, "/");
					strcpy (nombre, p);
					p=strtok(NULL,"/");
					char contrasenya[20];
					strcpy(contrasenya,p);
					printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);
					pu=0;
					printf("Nombre: %s, contrasenya: %s \n ",nombre, contrasenya);
					
					sprintf(str_query, "INSERT INTO jugadores VALUES ('%d','%s', '%s',%d);",id, nombre,contrasenya,pu);
					err=mysql_query (conn, str_query);
					
					
					if (err!=0)
					{
						
						printf ("Error al consultar datos de la base %u %s \n",
								mysql_errno(conn), mysql_error(conn));
						
							
					}
					sprintf(respuesta,"Bienvenido %s \n",nombre);
					write (sock_conn,respuesta, strlen(respuesta));
					mysql_close (conn);
					//exit(0);
					
				}
				else if (codigo ==3){ // consulta 1: dame el nombre del jugador con la maxima puntuacion
					//p = strtok( NULL, "/");
					//strcpy (nombre, p);
					printf ("Codigo: %d\n", nombre);
					strcpy(str_query,"SELECT jugadores.usuario FROM jugadores WHERE MAX(jugadores.puntuacion)");
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
							printf ("El jugador con mayor puntuacion es: %s\n", row [0] );
						if (row == NULL)
							printf ("No se han obtenido datos en la consulta\n");
						printf("\n");
						write (sock_conn,respuesta, strlen(respuesta));
						mysql_close (conn);
						exit(0);
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
					scanf("%s", nom);
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
						row = mysql_fetch_row (resultado); //FALTA CANVIAR DE CHAR A INT
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
			}
		}	
		// Se acabo el servicio para este cliente
		close(sock_conn);		
	}
	
}

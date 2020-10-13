FROM mcr.microsoft.com/mssql/server 

USER root

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Andreani_2020

WORKDIR /tmp
COPY ["geolocator/Persistence/setup.sql", "./"]
COPY ["geolocator/Persistence/import-data.sh", "./"]
COPY ["geolocator/Persistence/entrypoint.sh", "./"]


RUN chmod +x entrypoint.sh

CMD /bin/bash ./entrypoint.sh


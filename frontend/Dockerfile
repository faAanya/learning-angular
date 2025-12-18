FROM node:24.12.0-alpine

WORKDIR /auth

COPY auth/package*.json ./

RUN npm install -g @angular/cli
RUN npm install

COPY auth/ .

EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200"]

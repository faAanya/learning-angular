# Stage 1: Build Angular app
FROM node:22 AS build

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --configuration production

# Stage 2: Serve Angular app with nginx
FROM nginx:alpine

# Удаляем дефолтный nginx сайт
RUN rm -rf /usr/share/nginx/html/*

# Копируем сборку Angular в nginx
COPY --from=build /app/dist/auth /usr/share/nginx/html

# Копируем свой nginx конфиг для SPA
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

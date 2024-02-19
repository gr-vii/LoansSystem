# LoanManagementSystem
docker build . -t lms
 docker run -p 8081:80 -e ASPDOTNET_URLS=http://+:80 lms
 docker-compose up --build
  docker-compose down
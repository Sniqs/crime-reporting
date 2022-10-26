cd ..

docker build -t crime.azurecr.io/crimeapi -f .\CrimeApi\Dockerfile .
docker push crime.azurecr.io/crimeapi

docker build -t crime.azurecr.io/lawenforcementapi -f .\LawEnforcementApi\Dockerfile .
docker push crime.azurecr.io/lawenforcementapi

kubectl rollout restart all -n cr

pause 
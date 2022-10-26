kubectl apply -f CR-namespace.yml
kubectl apply -f CR-ingress-controller.yml
kubectl apply -f CR-ingress.yml
kubectl apply -f CR-secrets.yml
kubectl apply -f CR-configmaps.yml
kubectl apply -f CR-CrimeAPI-depl.yml
kubectl apply -f CR-LawEnforcementAPI-depl.yml
kubectl apply -f CR-seq-depl.yml
pause
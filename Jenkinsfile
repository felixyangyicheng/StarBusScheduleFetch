pipeline {
  environment {
    IMAGENAME = 'starbusschedulefetch'
    IMAGETAG = '1.0.0'
    APPPORT = '6089'
    APPDIR = '/opt/app'
  }
  agent none
  stages {
    stage('Build') {
      agent {
        docker {
          image 'mcr.microsoft.com/dotnet/sdk:9.0-alpine'
          args '-v /var/run/docker.sock:/var/run/docker.sock -v /usr/bin/docker:/usr/bin/docker'        
        }

      }
      steps {
        sh 'dotnet restore'
      }
    }

    stage('Publish') {
      agent {
        docker {
          image 'mcr.microsoft.com/dotnet/sdk:9.0-alpine'
          args '-v /var/run/docker.sock:/var/run/docker.sock -v /usr/bin/docker:/usr/bin/docker -v /var/jenkins_home/workspace/DotNetCoreJenkinsDemo_master@tmp:/StarBusScheduleFetch'
        }

      }
      steps {
        sh 'dotnet publish StarBusScheduleFetch -c Release'
      }
    }

    stage('Deploy') {
      agent any
      steps {
        sh 'touch Dockerfile'
        sh 'env'
        sh 'echo "start edit Dockerfile"'
        sh 'echo "FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine" >> Dockerfile'
        sh 'echo "COPY StarBusScheduleFetch/bin/Release/net9/publish ${APPDIR}" >> Dockerfile'
        sh 'echo "EXPOSE ${APPPORT}" >> Dockerfile'
        sh 'echo "WORKDIR ${APPDIR}" >> Dockerfile'
        sh 'echo \'ENTRYPOINT ["dotnet", "StarBusScheduleFetch.dll"]\' >> Dockerfile'
        sh 'cat Dockerfile'
        sh "docker build -t ${IMAGENAME}:${IMAGETAG} ."
      }
    }

  }

}

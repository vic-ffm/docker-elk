// TODO: Update this script to use `stash` -- seen in screenshot here: https://marketplace.visualstudio.com/items?itemName=jmMeessen.jenkins-declarative-support
timestamps {
	node {
		stage('Clone repository') {
			// Checkout the repo
			checkout scm
		}
    
		stage('Build Docker Image') {
			def releaseImage

			// Tag branches with their branch name if not master
			if( env.BRANCH_NAME == "master" ) {
				releaseImage = docker.build('elasticsearch', '--build-arg APP_PATH=${BUILD_TAG} ./elasticsearch')
				releaseImage = docker.build('kibana', '--build-arg APP_PATH=${BUILD_TAG} ./kibana')
				releaseImage = docker.build('logstash', '--build-arg APP_PATH=${BUILD_TAG} ./logstash')
			} else {
				def BRANCH_NAME_LOWER = BRANCH_NAME.toLowerCase().replaceAll(" ","_")
				// Non-env. variable needs double quotes to be parsed correctly (bash variables work better with single quotes)				
				elasticSearchImage = docker.build("elasticsearch_${BRANCH_NAME_LOWER}", '--build-arg APP_PATH=${BUILD_TAG} ./elasticsearch')
				kibanaImage = docker.build("kibana${BRANCH_NAME_LOWER}", '--build-arg APP_PATH=${BUILD_TAG} ./kibana')
				logstashImage = docker.build("logstash${BRANCH_NAME_LOWER}", '--build-arg APP_PATH=${BUILD_TAG} ./logstash')							}
        
			// Push the image with two tags:
			//  - Incremental build number from Jenkins
			//  - The 'latest' tag.
			// TODO: Complete creation of local docker registry (cert + credentials)
			docker.withRegistry('http://registry.ffm.vic.gov.au:31337/') {
				echo "Pushing Docker Image - ${env.BUILD_NUMBER}"
				elasticSearchImage.push("${env.BUILD_NUMBER}")
				kibanaImage.push("${env.BUILD_NUMBER}")
				logstashImage.push("${env.BUILD_NUMBER}")
				
				echo "Pushing Docker Image - latest"
				elasticSearchImage.push("latest")
				kibanaImage.push("latest")
				logstashImage.push("latest")
			}

			// Trigger release if this is master
			if( env.BRANCH_NAME == "master" ) {
			    build job: '../deploy-to-dev', parameters: [string(name: 'DOCKER_REPOSITORY', value: 'registry.ffm.vic.gov.au:31337'), string(name: 'ELASTICSEARCH_DOCKER_IMAGE', value: 'elasticsearch'), string(name: 'ELASTICSEARCH_DOCKER_TAG', value: "${env.BUILD_NUMBER}"), string(name: 'KIBANA_DOCKER_IMAGE', value: 'kibana'), string(name: 'KIBANA_DOCKER_TAG', value: "${env.BUILD_NUMBER}"), string(name: 'LOGSTASH_DOCKER_IMAGE', value: 'logstash'), string(name: 'LOGSTASH_DOCKER_TAG', value: "${env.BUILD_NUMBER}")], wait: false
			}
		}
	}
}
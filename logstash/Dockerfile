# https://github.com/elastic/logstash-docker
FROM docker.elastic.co/logstash/logstash-oss:6.0.0

# Add your logstash plugins setup here
# Example: RUN logstash-plugin install logstash-filter-json
RUN logstash-plugin install logstash-input-http

COPY ./config/logstash.yml /usr/share/logstash/config/logstash.yml
COPY ./pipeline/logstash.conf /usr/share/logstash/pipeline/logstash.conf
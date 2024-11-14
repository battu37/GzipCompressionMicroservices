# GzipCompressionMicroservices
between 2 microservices how compressed data will be handled.


The Employee Service can send requests with the Accept-Encoding: gzip header to the Department Service.
The Department Service will compress its responses, as requested.
When the Employee Service receives the compressed response from the Department Service, it will automatically decompress it, provided HttpClient supports gzip compression by default.

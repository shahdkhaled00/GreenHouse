from gradio_client import Client

client = Client("HagarEQAP99/AGRI_AGENT")

result = client.predict(
    "D:/back_plants/Greenhouse/wwwroot/uploads/WhatsApp Image 2025-04-25 at 11.36.26 PM.jpeg", 
    api_name="/handle_analysis",  
)

print(result)

import base64
import random

def otp_encrypt(message, key):
    encoded_message = message.encode('utf-8')
    key = bytes.fromhex(key)

    # Check if the key length matches the message length
    if len(key) != len(encoded_message):
        return "Error: Key length must match the message length."

    # Perform the XOR operation
    encrypted_bytes = bytes(x ^ y for x, y in zip(encoded_message, key))

    # Convert the result to a base64 encoded string
    encrypted_message = base64.b64encode(encrypted_bytes).decode('utf-8')

    return encrypted_message

def otp_decrypt(encrypted_message, key):
    key = bytes.fromhex(key)

    # Convert the base64 encoded message back to bytes
    encrypted_bytes = base64.b64decode(encrypted_message.encode('utf-8'))

    # Check if the key length matches the encrypted message length
    if len(key) != len(encrypted_bytes):
        return "Error: Key length must match the encrypted message length."

    # Perform the XOR operation to decrypt
    decrypted_bytes = bytes(x ^ y for x, y in zip(encrypted_bytes, key))

    # Convert the result to a string
    decrypted_message = decrypted_bytes.decode('utf-8')

    return decrypted_message

def find_key(encoded_message, decoded_message):
    encoded_bytes = base64.b64decode(encoded_message.encode('utf-8'))
    decoded_bytes = decoded_message.encode('utf-8')

    if len(encoded_bytes) != len(decoded_bytes):
        return "Error: Message lengths do not match."

    key = bytes(x ^ y for x, y in zip(encoded_bytes, decoded_bytes))
    return key.hex()

def caesar_encrypt(message, key):
    result = ''
    for char in message:
        if char.isalpha():
            shift = 65 if char.isupper() else 97
            result += chr((ord(char) - shift + key) % 26 + shift)
        else:
            result += char
    return result

def caesar_decrypt(encrypted_message, key):
    result = ''
    for char in encrypted_message:
        if char.isalpha():
            shift = 65 if char.isupper() else 97
            result += chr((ord(char) - shift - key) % 26 + shift)
        else:
            result += char
    return result

def permutare_encrypt(message, key):
    size = len(message)
    encrypted_message = ""
    for i in range(size):
        encrypted_message += message[key[i] - 1]
    return encrypted_message

def permutare_decrypt(encrypted_message, key):
    n = len(key)
    decrypted_message = [""] * n
    for i in range(n):
        decrypted_message[i] = encrypted_message[key.index(i + 1)]
    result = ""
    return result.join(decrypted_message)


# Input the base64 encoded message and hexadecimal key
encrypted_message = "o9/khC3Pf3/9CyNCbdzHPy5oorccEawZSFt3mgCicRnihDSM8Obhlp3vviAVuBbiOtCSz6husBWqhfF0Q/8EZ+6iI9KygD3hAfFgnzyv9w==="
key_hex = "ecb181a479a6121add5b42264db9b44b4b48d7d93c62c56a3c3e1aba64c7517a90ed44f8919484b6ed8acc4670db62c249b9f5bada4ed474c9e4d111308b614788cd4fbdc1e949c1629e12fa5fdbd9"
key_hex2 = "ecad8de748ef0b1a857f032101bdb51f5e07c3c37931c37b3c3219ef748215708cf046a18588c1e2f897ca0076ca7f924eb1e6efcb1b905afed5d110228d24049b824cf2d3ec4980219208fa55cad9"
decrypted_message = otp_decrypt(encrypted_message, key_hex2)

encoded_message = "o9/khC3Pf3/9CyNCbdzHPy5oorccEawZSFt3mgCicRnihDSM8Obhlp3vviAVuBbiOtCSz6husBWqhfF0Q/8EZ+6iI9KygD3hAfFgnzyv9w=="
decoded_message = "Orice text clar poate obtinut dintr-un text criptat cu OTP dar cu o alta cheie."

message_Caesar = "Codarea Caesar ne-a fost mentionata in primul curs de SSI"
key_Caesar = 3

message_Permutare = "Codarea prin permutare este o alta metoda de criptare"
key_aux = list(range(1, len(message_Permutare) + 1))
random.shuffle(key_aux)
key_Permutare = key_aux

#print("Key Permutare: ", key_Permutare)
#encrypted_message_permutare = permutare_encrypt(message_Permutare, key_Permutare)
#print("Permutare Encrypted Message: ", encrypted_message_permutare)
#print("Permutare Decrypted Message: ", permutare_decrypt(encrypted_message_permutare, key_Permutare))

#print("Encrypted Message: ", encrypted_message)
#print("Decrypted Message: ", decrypted_message)
#print("Key: ", find_key(encoded_message, decoded_message))

encoded_Caesar = "ENHFJ EWK LML EOJ GDJ BMONKC PMCG YEPMAC"
key_Caesar_2 = 2
#encoded_Caesar = caesar_encrypt(message_Caesar, key_Caesar)
#print("Caesar Cipher Encoded Message: ", caesar_encrypt(message_Caesar, key_Caesar))
print("Caesar Cipher Decoded Message: ", caesar_decrypt(encoded_Caesar, key_Caesar_2))






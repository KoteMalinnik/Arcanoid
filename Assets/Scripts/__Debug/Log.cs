using System.Text;

/// <summary>
/// Логирование в консоль Unity.
/// </summary>
public static class Log
{
	/// <summary>
	/// Выведет сообщение message в консоль Unity.
	/// </summary>
	/// <param name="message">Сообщение.</param>
	public static void Message(params object[] message)
	{
#if UNITY_EDITOR
		UnityEngine.Debug.Log(createMessage(message));
#endif
	}

	/// <summary>
	/// Выведет предупреждение message в консоль Unity.
	/// </summary>
	/// <param name="message">Сообщение.</param>
	public static void Warning(params object[] message)
	{
#if UNITY_EDITOR
		UnityEngine.Debug.LogWarning($"<color=yellow>{createMessage(message)}</color>");
#endif
	}

	/// <summary>
	/// Выведет ошибку с сообщением message в консоль Unity.
	/// </summary>
	/// <param name="message">Сообщение.</param>
	public static void Error(params object[] message)
	{
#if UNITY_EDITOR
		UnityEngine.Debug.LogError($"<color=red>{createMessage(message)}</color>");
		UnityEngine.Debug.Break();
#endif
	}

	static string createMessage(params object[] message)
    {
		StringBuilder messageToShow = new StringBuilder();

		for (int i = 0; i < message.Length; i++)
		{
			messageToShow = messageToShow.AppendLine(message[i].ToString());
		}

		return messageToShow.ToString();
	}
}
